using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorPositions : MonoBehaviour
{
    
    public static ActorPositions Instance;

    public GameObject Player;
    public GameObject Female;
    public GameObject Male;

    private Vector3 PlayerBackseatPosition = new Vector3(9.44f, -4.31f, 18.51f);
    private Vector3 PlayerBackseatScale = new Vector3(14.6f,14.6f,14.6f);
    private Vector3 MaleBackseatPosition = new Vector3(7.94f, 5.72f, 17.24f);
    private Vector3 MaleShotgunPosition = new Vector3(-4.51f, 4.49f, 5.04f);
    private Vector3 MaleShotgunScale = new Vector3(15, 15, 15);
    
    private Vector3 PlayerShotgunPosition;
    private Vector3 PlayerShotgunScale;

    void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        PlayerShotgunPosition = Player.transform.position;
        PlayerShotgunScale = Player.transform.localScale;
        PlayerToBackseat();
    }

    void PlayerToBackseat()
    {
        Player.transform.position = PlayerBackseatPosition;
        Player.transform.localScale = PlayerBackseatScale;
        Male.transform.position = MaleShotgunPosition;
        Male.transform.localScale = MaleShotgunScale;
    }

    void PlayerToShotgun()
    {
        Player.transform.position = PlayerShotgunPosition;
        Player.transform.localScale = PlayerShotgunScale;
        Male.transform.position = MaleBackseatPosition;
        //Male.transform.localScale = 
    }
}
