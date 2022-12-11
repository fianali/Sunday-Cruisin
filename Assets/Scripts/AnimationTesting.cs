using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTesting : MonoBehaviour
{

    public static AnimationTesting Instance;
    
    public AudioSource Male;

    public AudioClip WelcomeDialogue;
    public AudioClip LookedAtWindowDialogue;
    public AudioClip GiveMeSomeCrackersDialogue;

    public Animator Animator;


    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        // Male.clip = WelcomeDialogue;
        // Male.Play();
        
        // Animator.SetBool("Welcoming", true);
        // Animator.SetBool("Twisted", true);
        // StartCoroutine(WelcomingRevert());

        // GameController.Instance.fed = false;
    }

    void Update()
    {
        if (TerrainGenerator.Instance.finished)
        {
            Male.clip = WelcomeDialogue;
            Male.Play();
        
            Animator.SetBool("Welcoming", true);
            Animator.SetBool("Twisted", true);
            StartCoroutine(WelcomingRevert());

            GameController.Instance.fed = false;
            TerrainGenerator.Instance.finished = false;
        }
    }

    public void LookedAtWindow()
    {
        Male.clip = LookedAtWindowDialogue;
        Male.Play();
        
        Animator.SetBool("LookedAtWindow", true);
        Animator.SetBool("Twisted", true);
        StartCoroutine(LookedAtWindowRevert());
    }

    public void GiveMeCrackers()
    {
        Male.clip = GiveMeSomeCrackersDialogue;
        Male.Play();
        
        Animator.SetBool("Hungry", true);
    }

    public void GiveMeCrackersRevert()
    {
        Animator.SetBool("Hungry", false);
    }

    IEnumerator WelcomingRevert()
    {
        yield return new WaitForSeconds(2f);
        Animator.SetBool("Welcoming", false);
        yield return new WaitForSeconds(5f);
        Animator.SetBool("Twisted", false);
    }

    IEnumerator LookedAtWindowRevert()
    {
        yield return new WaitForSeconds(2f);
        Animator.SetBool("LookedAtWindow", false);
        Animator.SetBool("Hungry", true);
        yield return new WaitForSeconds(10f);
        Animator.SetBool("Twisted", false);
        
        GameController.Instance.introOver = true;
    }
}
