using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] private GameObject promoteText;
    [SerializeField] private GameObject crackerInstructions;
    [SerializeField] private GameObject passenger;
    [SerializeField] private GameObject driver;
    [SerializeField] private GameObject likeDislike;


    public bool liked = true;

    void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        CheckPromotion();
        GetSongPreference();
        GiveCrackerInstruction();
        DisplayPassengerUI();
        DisplayDriverUI();
        HideLikeDislike();
    }
    
    void CheckPromotion()
    {
        //promote to passenger, might need to change cause i changed foodcount back to 0;
        if (GameController.Instance.promote == true && GameController.Instance.backseat == true)
        {
            promoteText.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                promoteText.SetActive(false);
                GameController.Instance.foodCount = 0;
            }
        }
        
        //just for prerelease, if player changes radio station they promote
        if (GameController.Instance.promote == true && GameController.Instance.passenger == true)
        {
            promoteText.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                promoteText.SetActive(false);
                driver.SetActive(true);
            }
        }
    }

    void GiveCrackerInstruction()
    {
        if (GameController.Instance.startCount && GameController.Instance.backseat)
        {
            crackerInstructions.SetActive(true);
        }

        if (!GameController.Instance.backseat)
        {
            crackerInstructions.SetActive(false);
        }
    }

    void GetSongPreference()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Debug.Log("you like this song");
            liked = true;
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Debug.Log("you dont like this song");
            liked = false;
        }
    }

    void DisplayPassengerUI()
    {
        if (GameController.Instance.passenger == true)
        {
            passenger.SetActive(true);
        }
        else
        {
            passenger.SetActive(false);
        }
    }

    void DisplayDriverUI()
    {
        if (GameController.Instance.driver == true)
        {
            driver.SetActive(true);
        }
        else
        {
            driver.SetActive(false);
        }
    }

    void HideLikeDislike()
    {
        if (GameController.Instance.passenger == true)
        {
            likeDislike.SetActive(false);
        }
        else
        {
            likeDislike.SetActive(true);
        }
    }
}
