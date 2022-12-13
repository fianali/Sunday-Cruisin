using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimationTesting : MonoBehaviour
{
    public static AnimationTesting Instance;
    
    public AudioSource Male;
    public AudioSource Female;

    public AudioClip WelcomeDialogue;
    public AudioClip LookedAtWindowDialogue;
    public AudioClip GiveMeSomeCrackersDialogue;
    public AudioClip ThumbsDialogue;
    public AudioClip SucksDialogue;
    public AudioClip AwesomeDialogue;

    public Animator MaleAnimator;
    public Animator FemaleAnimator;
    public Animator PlayerAnimator;

    private UnityEvent terrainReady;

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
        terrainReady = TerrainLoader.Instance.terrainReady;
        terrainReady.AddListener(OnTerrainReady);

        Male.clip = WelcomeDialogue;
        Male.Play();
        
        MaleAnimator.SetBool("Welcoming", true);
        MaleAnimator.SetBool("Twisted", true);
        StartCoroutine(WelcomingRevert());
    }

    void OnTerrainReady()
    {
        // GameController.Instance.fed = false;
    }

    void Update()
    {
        /*if (TerrainGenerator.Instance.finished)
        {
            Male.clip = WelcomeDialogue;
            Male.Play();
        
            MaleAnimator.SetBool("Welcoming", true);
            MaleAnimator.SetBool("Twisted", true);
            StartCoroutine(WelcomingRevert());

            GameController.Instance.fed = false;
            TerrainGenerator.Instance.finished = false;
        }*/
    }

    
    ///////////////////////////////// MALE ////////////////////////////////////
    
    public void LookedAtWindow()
    {
        Male.clip = LookedAtWindowDialogue;
        Male.Play();
        
        MaleAnimator.SetBool("LookedAtWindow", true);
        MaleAnimator.SetBool("Twisted", true);
        StartCoroutine(LookedAtWindowRevert());
    }

    public void GiveMeCrackers()
    {
        Male.clip = GiveMeSomeCrackersDialogue;
        Male.Play();
        
        MaleAnimator.SetBool("Hungry", true);
    }

    public void GiveMeCrackersRevert()
    {
        MaleAnimator.SetBool("Hungry", false);
    }

    

    IEnumerator WelcomingRevert()
    {
        yield return new WaitForSeconds(2f);
        MaleAnimator.SetBool("Welcoming", false);
        yield return new WaitForSeconds(5f);
        MaleAnimator.SetBool("Twisted", false);
    }

    IEnumerator LookedAtWindowRevert()
    {
        yield return new WaitForSeconds(2f);
        MaleAnimator.SetBool("LookedAtWindow", false);
        MaleAnimator.SetBool("Hungry", true);
        yield return new WaitForSeconds(2f);
        GameController.Instance.fed = false;
        GameController.Instance.startCount = true;
        yield return new WaitForSeconds(10f);
        MaleAnimator.SetBool("Twisted", false);
        
        GameController.Instance.introOver = true;
    }
    
    
   /////////////////////// FEMALE //////////////////////////
    
    
    public void HowDoYouLikeTheMusic()
    {
        FemaleAnimator.SetBool("Twisted", true);
        Female.clip = ThumbsDialogue;
        Female.Play();
        
        FemaleAnimator.SetBool("Thumbs", true);
        StartCoroutine(HowDoYouLikeTheMusicRevert());
    }

    public void ThisMusicSucks()
    {
        FemaleAnimator.SetBool("Twisted", true);
        Female.clip = SucksDialogue;
        Female.Play();
        
        FemaleAnimator.SetBool("Sucks", true);
        StartCoroutine(ThisMusicSucksRevert());
    }
    
    public void ThisMusicIsAwesomeness()
    {
        FemaleAnimator.SetBool("Twisted", true);
        Female.clip = AwesomeDialogue;
        Female.Play();
        
        FemaleAnimator.SetBool("Awesome", true);
        StartCoroutine(ThisMusicIsAwesomenessRevert());
    }

    IEnumerator HowDoYouLikeTheMusicRevert()
    {
        yield return new WaitForSeconds(2f);
        FemaleAnimator.SetBool("Thumbs", false);
        yield return new WaitForSeconds(2f);
        FemaleAnimator.SetBool("Twisted", false);
    }

    IEnumerator ThisMusicSucksRevert()
    {
        yield return new WaitForSeconds(.2f);
        FemaleAnimator.SetBool("Sucks", false);
        yield return new WaitForSeconds(2f);
        FemaleAnimator.SetBool("Twisted", false);
    }
    
    IEnumerator ThisMusicIsAwesomenessRevert()
    {
        yield return new WaitForSeconds(.2f);
        FemaleAnimator.SetBool("Awesome", false);
        yield return new WaitForSeconds(2f);
        FemaleAnimator.SetBool("Twisted", false);
    }
    
    IEnumerator TestingTimer()
    {
        yield return new WaitForSeconds(2f);
        HowDoYouLikeTheMusic();
    }
}
