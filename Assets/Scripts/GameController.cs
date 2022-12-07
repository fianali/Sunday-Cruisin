using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class GameController : MonoBehaviour
{

    // probably gonna split all of this into multiple scripts eventually

    public static GameController Instance;
    
    public bool promote;
    public bool demote;

    public bool backseat;
    public bool passenger;
    public bool driver;

    public bool fed = true;
    private float timeWaited = 0f;

    public int foodCount;
    private int songCount;
    private bool pow = true;

    private bool likedSong = true;
    private bool timesUp = false;

    private bool hungry;

    public bool introOver = false;
    private bool startCount = false;

    
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
            Debug.Log("Start");
            StartCoroutine(Food());
            introOver = false;
            startCount = true;
        }
        
        if (!likedSong)
        {
            // station change
        }

        if (!backseat)
        {
            // if (click ask for snack?)
            // {
            //    hungry = true;
            // }
            // if (hungry)
            // {
            //    if (passenger)
            //    {
            //       food given to passenger
            //    }
            //    if (driver)
            //    {
            //       food given to driver
            //    }
            // }
        }
        if (backseat)
        {
            if (!fed)
            {
                timeWaited = Time.time;
            
                // if (give food)
                //   fed = true

                if (Input.GetMouseButtonDown(0))
                {
                    fed = true;
                    Debug.Log("YOU GAVE ME CRACKERS");
                    AnimationTesting.Instance.GiveMeCrackersRevert();
                }
            }
            else if (fed && startCount)
            {
                if (timeWaited < 3f && pow)
                {
                    foodCount++;
                    pow = false;
                    Debug.Log(foodCount);
                }
                timeWaited = 0f;
            }

            if (foodCount >= 3)
            {
                Debug.Log("Promote");
                promote = true;
                backseat = false;
                passenger = true;
            }
        }

        if (!passenger)
        {
            //if (click up)
            // {
            //    likedSong = true
            // } 
            //if (click down)
            // {
            //    likedSong = false
            // }
        }
        // if (passenger)
        else
        {
            // yeahhhh turn it up

            // random feedback?
            
            // if (!likedSong)
            // {
            //    StartCoroutine(Music())
            //    if (click on change song? if play specific radio? do these ppl have specific preferences idk)
            //    { 
            //        likedSong = true
            //        songCount++;
            //    }
            // }
            // 
            if (timesUp && !likedSong)
            {
                timesUp = false;
                songCount--;
            }
            if (songCount == -1)
            {
                demote = true;
                songCount = 0;
            }
            if (songCount == 5)
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
            // if (backseat)
            //   move to passenger seat (passenger = true)
            // if (passenger)
            //   move to driver's seat (driver = true)
        }
        else if (demote)
        {
            // if (driver)
            //   move to passenger seat (passenger = true)
            // if (passenger)
            //   move to backseat (backseat = true)
        }
    }

    IEnumerator Food()
    {
        while (!passenger)
        {
            int rand = Random.Range(20, 30);

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
        // requests???? idfk
        
        
        yield return new WaitForSeconds(5);
        timesUp = true;
    }
    
}
