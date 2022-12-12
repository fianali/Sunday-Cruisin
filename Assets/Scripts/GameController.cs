using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class GameController : MonoBehaviour
{

    // probably gonna split all of this into multiple scripts eventually

    public static GameController Instance;
    
    public bool promote = false;
    public bool demote;

    public bool backseat = true;
    public bool passenger = false;
    public bool driver = false;

    public bool fed = true;
    private float timeWaited = 0f;

    public int foodCount;
    private int songCount = 0;
    private bool pow = true;
    private bool pow2 = true;

    private bool likedSong = false;
    private bool timesUp = false;

    private bool hungry;

    public bool introOver = false;
    private bool startCount = false;
    private bool checkRequest = true;

    
    void Awake()
    {
        Instance = this;
        backseat = true;
    }

    void Start()
    {
        
    }
    
    void Update()
    {

        if (introOver)
        {
            Debug.Log("INTRO OVER");
            StartCoroutine(Food());
            introOver = false;
            startCount = true;
        }
        
        if (backseat)
        {
            if (!fed)
            {    
                timeWaited += Time.deltaTime;

                if (Input.GetMouseButtonDown(0))
                {
                    fed = true;
                    
                    AnimationTesting.Instance.GiveMeCrackersRevert();
                }
            }
            else if (fed && startCount)
            {
                if (timeWaited < 6f && pow)
                {
                    foodCount++;
                    Debug.Log(foodCount);
                    pow = false;
                    
                }
                else if (timeWaited > 7f && pow)
                {
                    foodCount--;
                    Debug.Log(foodCount);
                    pow = false;
                }
                timeWaited = 0f;
            }

            if (foodCount >= 3)
            {
                promote = true;
            }
        }
      
        if (passenger)
        {
            if (pow2)
            {
                SoundController.Instance.songChanged = false;
                pow2 = false;
            }
            

            if (checkRequest || SoundController.Instance.songChanged)
            {
                SoundController.Instance.songChanged = false;

                int rand = Random.Range(0, 2);
                if (rand == 1)
                {
                    likedSong = false;
                    StartCoroutine(Music());
                }
                else
                {
                    likedSong = true;
                    StartCoroutine(GoodMusic());
                    songCount++;
                }

                
                checkRequest = false;
            }
            
            

            if (timesUp && !likedSong)
            {
                timesUp = false;
                songCount--;
            }
            if (songCount == -3)
            {
                demote = true;
                backseat = true;
                songCount = 0;
                
            }
            if (songCount == 4)
            {
                promote = true;
                songCount = 0;
                
            }
        }

        if (driver)
        {
            // drive
        }
        
        if (promote)
        {
            if (passenger && !backseat)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    ActorPositions.Instance.PlayerToDriver();
                    passenger = false;
                    driver = true;
                    promote = false;
                }

            }
            if (backseat)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    ActorPositions.Instance.PlayerToShotgun();
                    backseat = false;
                    passenger = true;
                    promote = false;
                }
            }
            
        }
        else if (demote)
        {
            if (driver)
            {
                ActorPositions.Instance.PlayerToShotgun();
                passenger = true;
                driver = false;
                demote = false;
            }
            if (passenger)
            {
                ActorPositions.Instance.PlayerToBackseat();
                backseat = true;
                passenger = false;
                demote = false;
                StartCoroutine(Food());
            }
        }
    }

    IEnumerator Food()
    {
        while (backseat)
        {
            int rand = Random.Range(10, 15);

            if (fed)
            {
                AnimationTesting.Instance.GiveMeCrackers();
                pow = true;
            }
            

            fed = false;
            yield return new WaitForSeconds(rand);
        }
    }
    
    
    IEnumerator Music()
    {
        yield return new WaitForSeconds(5);
        AnimationTesting.Instance.ThisMusicSucks();

        yield return new WaitForSeconds(10);
        if (SoundController.Instance.songChangedInTime)
        {
            SoundController.Instance.songChangedInTime = false;
            yield break;
        }
        timesUp = true;
         
    } 

    IEnumerator GoodMusic() 
    {
        yield return new WaitForSeconds(5);
        AnimationTesting.Instance.ThisMusicIsAwesomeness();
    }
}
