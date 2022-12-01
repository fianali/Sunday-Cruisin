using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class GameController : MonoBehaviour
{

    // probably gonna split all of this into multiple scripts eventually
    
    
    public bool promote;
    public bool demote;

    private bool backseat;
    private bool passenger;
    private bool driver;

    private bool fed = true;
    private float timeWaited = 0f;

    private int foodCount;
    private bool pow;

    void Start()
    {
        StartCoroutine(Food());
    }
    
    void Update()
    {
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

        if (passenger)
        {
            // yeahhhh turn it up
        }

        if (driver)
        {
            // drive
        }

       
        
        if (promote)
        {
            // if (backseat)
            //   move to passenger seat
            // if (passenger)
            //   move to driver's seat
        }
        else if (demote)
        {
            // if (driver)
            //   move to passenger seat
            // if (passenger)
            //   move to backseat
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
    }
    
}
