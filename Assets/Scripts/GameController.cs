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

    private bool backseat;
    private bool passenger;
    private bool driver;

    private bool fed = true;
    private float timeWaited = 0f;

    private int foodCount;
    private int songCount;
    private bool pow;

    private bool likedSong = true;
    private bool timesUp = false;

    private bool hungry;
    
    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        StartCoroutine(Food());
    }
    
    void Update()
    {
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
            }
            else if (fed)
            {
                if (timeWaited < 3f)
                {
                    foodCount++;
                }
                timeWaited = 0f;
            }

            if (foodCount >= 3)
            {
                promote = true;
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
        while (true)
        {
            int rand = Random.Range(5, 10);

            // if (fed)
            //   ask for food
        
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
