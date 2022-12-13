
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    public static SoundController Instance;
    
    public AudioSource radio;

    public AudioClip one;
    public AudioClip two;
    public AudioClip three;
    public AudioClip four;
    public AudioClip five;

    public AudioClip one2;
    public AudioClip two2;
    public AudioClip three2;
    public AudioClip four2;
    public AudioClip five2;

    public AudioClip one3;
    public AudioClip two3;
    public AudioClip three3;
    public AudioClip four3;
    public AudioClip five3;

    public AudioClip one4;
    public AudioClip two4;
    public AudioClip three4;
    public AudioClip four4;
    public AudioClip five4;

    private AudioClip[] first;
    private AudioClip[] second;
    private AudioClip[] third;
    private AudioClip[] fourth;

    private bool onePressed = false;
    private bool twoPressed = false;
    private bool threePressed = false;
    private bool fourPressed = false;

    public bool songUp = false;
    public bool songDown = false;

    private int count = 0;
    private bool pow = false;

    public bool songChanged = false;
    public bool songChangedInTime = false;

    private bool start = true;

    public bool[] stations;
    public int index = 0;
    
    void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        stations = new bool[4];
        stations[0] = false;
        stations[1] = false;
        stations[2] = false;
        stations[3] = false;

        first = new AudioClip[5];
        first[0] = one;
        first[1] = two;
        first[2] = three;
        first[3] = four;
        first[4] = five;

        second = new AudioClip[5];
        second[0] = one2;
        second[1] = two2;
        second[2] = three2;
        second[3] = four2;
        second[4] = five2;

        third = new AudioClip[5];
        third[0] = one3;
        third[1] = two3;
        third[2] = three3;
        third[3] = four3;
        third[4] = five3;

        fourth = new AudioClip[5];
        fourth[0] = one4;
        fourth[1] = two4;
        fourth[2] = three4;
        fourth[3] = four4;
        fourth[4] = five4;

       
    }


    void Update()
    {
        if (GameController.Instance.passenger)
        {
             // change station
            if (Input.GetKeyDown(KeyCode.Alpha1) || stations[0])
            {
                count = 0;
                pow = true;
                stations[0] = false;

                onePressed = true;
                twoPressed = false;
                threePressed = false;
                fourPressed = false;

                radio.clip = first[count];
                radio.Play();

                songChanged = true;
                songChangedInTime = true;
                start = false;
               
            }
            if (Input.GetKeyDown(KeyCode.Alpha2) || stations[1])
            {
                count = 0;
                pow = true;
                stations[1] = false;

                onePressed = false;
                twoPressed = true;
                threePressed = false;
                fourPressed = false;

                radio.clip = second[count];
                radio.Play();

                songChanged = true;
                songChangedInTime = true;
            }
            if (Input.GetKeyDown(KeyCode.Alpha3) || stations[2])
            {
                count = 0;
                pow = true;
                stations[2] = false;

                onePressed = false;
                twoPressed = false;
                threePressed = true;
                fourPressed = false;

                radio.clip = third[count];
                radio.Play();

                songChanged = true;
                songChangedInTime = true;
            }
            if (Input.GetKeyDown(KeyCode.Alpha4) || stations[3])
            {
                count = 0;
                pow = true;
                stations[3] = false;

                onePressed = false;
                twoPressed = false;
                threePressed = false;
                fourPressed = true;

                radio.clip = fourth[count];
                radio.Play();

                songChanged = true;
                songChangedInTime = true;
            }

            // change song
            if (Input.GetKeyDown(KeyCode.RightArrow) || songUp)
            {
                songChanged = true;
                songChangedInTime = true;
                songUp = false;

                count++;
                if (count == 5)
                {
                    count = 0;
                }

                if (onePressed)
                {
                    radio.clip = first[count];
                }
                if (twoPressed)
                {
                    radio.clip = second[count];
                }
                if (threePressed)
                {
                    radio.clip = third[count];
                }
                if (fourPressed)
                {
                    radio.clip = fourth[count];
                }

                radio.Play();
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow) || (!radio.isPlaying && pow) || songDown)
            {
                songChanged = true;
                songChangedInTime = true;
                songDown = false;

                count--;
                if (count == -1)
                {
                    count = 4;
                }

                if (onePressed)
                {
                    radio.clip = first[count];
                }
                if (twoPressed)
                {
                    radio.clip = second[count];
                }
                if (threePressed)
                {
                    radio.clip = third[count];
                }
                if (fourPressed)
                {
                    radio.clip = fourth[count];
                }

                radio.Play();
            }
        }
        else
        {
            if (!UIManager.Instance.liked || start)
            {
                UIManager.Instance.liked = true;
                radio.Stop();
                start = false;

                int randStation = Random.Range(0, 4);
                int randSong = Random.Range(0, 5);


                count = randSong;

                if (randStation == 0)
                {
                    radio.clip = first[randSong];
                    onePressed = true;
                    twoPressed = false;
                    threePressed = false;
                    fourPressed = false;
                }
                if (randStation == 1)
                {
                    radio.clip = second[randSong];
                    onePressed = false;
                    twoPressed = true;
                    threePressed = false;
                    fourPressed = false;
                }
                if (randStation == 2)
                {
                    radio.clip = third[randSong];
                    onePressed = false;
                    twoPressed = false;
                    threePressed = true;
                    fourPressed = false;
                }
                if (randStation == 3)
                {
                    radio.clip = fourth[randSong];
                    onePressed = false;
                    twoPressed = false;
                    threePressed = false;
                    fourPressed = true;
                }
            
                radio.Play();
            }
        }
    }
}