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

    private int badSongCount = 0;

    
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

            if (foodCount >= 1)
            {
                promote = true;
            }
        }
      
        if (passenger)
        {
            if (badSongCount >= 1)
            {
                demote = true;
            }
            
            if (pow2)
            {
                SoundController.Instance.songChanged = false;
                pow2 = false;
            }
            

            if (checkRequest || SoundController.Instance.songChanged)
            {
                StopCoroutine(Music());
                StopCoroutine(GoodMusic());
                
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
                }
                checkRequest = false;
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
                    foodCount = 0;
                    badSongCount = 0;
                    promote = false;
                    demote = false;
                }

            }
            if (backseat)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    ActorPositions.Instance.PlayerToShotgun();
                    backseat = false;
                    passenger = true;
                    badSongCount = 0;
                    promote = false;
                    demote = false;
                }
            }
            
        }
        if (demote)
        {
            if (driver)
            {
                ActorPositions.Instance.PlayerToShotgun();
                passenger = true;
                driver = false;
                demote = false;
                promote = false;
            }
            if (passenger)
            {
                ActorPositions.Instance.PlayerToBackseat();
                backseat = true;
                passenger = false;
                badSongCount = 0;
                demote = false;
                promote = false;
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
        badSongCount += 1;
    } 

    IEnumerator GoodMusic() 
    {
        yield return new WaitForSeconds(5);
        AnimationTesting.Instance.ThisMusicIsAwesomeness();
        yield return new WaitForSeconds(10);
        promote = true;

    }
}
