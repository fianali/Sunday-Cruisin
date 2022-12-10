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
    
    private Vector3 PlayerBackseatPosition = new Vector3(9.67f, -2.1f, 17.63f);
    private Vector3 PlayerBackseatScale = new Vector3(12.4f,12.4f,12.4f);
    
    private Vector3 MaleShotgunPosition = new Vector3(-5.43f, 3.35f, 1.41f);
    private Vector3 MaleShotgunScale = new Vector3(15.8f, 15.8f, 15.8f);
    
    private Vector3 PlayerShotgunPosition;
    private Vector3 PlayerShotgunScale;
    private Vector3 MaleBackseatPosition;
    private Vector3 MaleBackseatScale;
    
    void Awake()
    {
        Instance = this;
    }
    
    private void Start()
    {
        PlayerShotgunPosition = Player.transform.localPosition;
        PlayerShotgunScale = Player.transform.localScale;
        MaleBackseatPosition = Male.transform.localPosition;
        MaleBackseatScale = Male.transform.localScale;
        PlayerToBackseat();
    }
    
    public void PlayerToBackseat()
    {
        Debug.Log("player to backseat");
        Player.transform.localPosition = PlayerBackseatPosition;
        Player.transform.localScale = PlayerBackseatScale;
        Male.transform.localPosition = MaleShotgunPosition;
        Male.transform.localScale = MaleShotgunScale;
    }
    
    public void PlayerToShotgun()
    {
        Debug.Log("Player to Shotgun Fired");
        Player.transform.localPosition = PlayerShotgunPosition;
        Player.transform.localScale = PlayerShotgunScale;
        Male.transform.localPosition = MaleBackseatPosition;
        Male.transform.localScale = MaleBackseatScale;
    }
}
