using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject promoteText;
    [SerializeField] private GameObject crackerInstructions;
    [SerializeField] private GameObject passenger;
    [SerializeField] private GameObject driver;




    private void Update()
    {
        CheckPromotion();
        GetSongPreference();
        GiveCrackerInstruction();
        DisplayPassengerUI();
    }
    
    void CheckPromotion()
    {
        //promote to passenger, might need to change cause i changed foodcount back to 0;
        if (GameController.Instance.promote == true && GameController.Instance.foodCount >= 3)
        {
            promoteText.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                promoteText.SetActive(false);
                GameController.Instance.foodCount = 0;
                Debug.Log("you have promoted to passenger!");
            }
        }
        
        //just for prerelease, if player changes radio station they promote
        if (GameController.Instance.promote == true && GameController.Instance.passenger == true)
        {
            promoteText.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                driver.SetActive(true);
                Debug.Log("youre now driving! skrrrrrt");
            }
        }
    }

    void GiveCrackerInstruction()
    {
        if (GameController.Instance.introOver == true)
        {
            crackerInstructions.SetActive(true);
        }

        if (GameController.Instance.foodCount >= 3)
        {
            crackerInstructions.SetActive(false);
        }
    }

    void GetSongPreference()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Debug.Log("you like this song");
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Debug.Log("you dont like this song");
        }
    }

    void DisplayPassengerUI()
    {
        if (GameController.Instance.passenger == true && GameController.Instance.foodCount == 0)
        {
            passenger.SetActive(true);
        }
    }

    void DisplayDriverUI()
    {
        
    }
}
