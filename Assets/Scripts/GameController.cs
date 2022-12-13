using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class GameController : MonoBehaviour
{

    public static GameController Instance;
    
    public bool promote = false;
    public bool demote = false;

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
    public bool startCount = false;
    private bool checkRequest = true;
    private bool hasFedOnce = false;

    private int badSongCount = 0;
    private int goodSongCount = 0;

    public int backseatPromo = 1;
    public int passengerPromo = 1;

    public GameObject car;
    public Rigidbody rb;

    
    void Awake()
    {
        Instance = this;
        backseat = true;
    }

    void Start()
    {
        //Rigidbody rb = car.GetComponent<Rigidbody>();
    }
    
    void Update()
    {
        Debug.Log(startCount);

        if (introOver)
        {
            StartCoroutine(Food());
            introOver = false;
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
                    if (hasFedOnce == false)
                    {
                        hasFedOnce = true;
                        AnimationTesting.Instance.HowDoYouLikeTheMusic();
                    }
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

            if (foodCount >= backseatPromo)
            {
                promote = true;
            }
        }
      
        if (passenger)
        {
            if (badSongCount >= passengerPromo)
            {
                demote = true;
            }
            if (goodSongCount >= passengerPromo)
            {
                promote = true;
            }

            
            if (pow2)
            {
                SoundController.Instance.songChanged = false;
                pow2 = false;
            }
            

            if (checkRequest || SoundController.Instance.songChanged)
            {
                StopAllCoroutines();
                
                SoundController.Instance.songChanged = false;

                int rand = Random.Range(0, 2);
                if (rand == 1)
                {
                    likedSong = false;
                    SoundController.Instance.songChangedInTime = false;
                    StartCoroutine(Music());
                }
                else
                {
                    likedSong = true;
                    SoundController.Instance.songChangedInTime = false;
                    StartCoroutine(GoodMusic());
                }
                checkRequest = false;
            }
        }

        if (driver)
        {
            if (rb.velocity.x <= 1 && rb.velocity.z <= 1)
            {
                demote = true;
            }
        }
        
        if (promote)
        {
            if (passenger)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    ActorPositions.Instance.PlayerToDriver();
                    passenger = false;
                    driver = true;
                    foodCount = 0;
                    badSongCount = 0;
                    goodSongCount = 0;
                    promote = false;
                    demote = false;
                }

            }
            if (backseat)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    ActorPositions.Instance.PlayerToShotgun();
                    checkRequest = true;
                    backseat = false;
                    passenger = true;
                    promote = false;
                    demote = false;
                }
            }
            
        }
        if (demote)
        {
            if (passenger && promote == false)
            {
                ActorPositions.Instance.PlayerToBackseat();
                backseat = true;
                passenger = false;
                badSongCount = 0;
                goodSongCount = 0;
                demote = false;
                StartCoroutine(Food());
            }
            if (driver)
            {
                ActorPositions.Instance.PlayerToShotgun();
                checkRequest = true;
                passenger = true;
                driver = false;
                demote = false;
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
        Debug.Log("badSongCount added");
        badSongCount += 1;
        } 

    IEnumerator GoodMusic() 
    {
        yield return new WaitForSeconds(5);
        AnimationTesting.Instance.ThisMusicIsAwesomeness();
        yield return new WaitForSeconds(10);
        Debug.Log("goodSongCount added");
        goodSongCount += 1;
        }
}
