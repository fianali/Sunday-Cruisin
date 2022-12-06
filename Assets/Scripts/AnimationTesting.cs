using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTesting : MonoBehaviour
{
    
    public AudioSource Male;

    public AudioClip WelcomeDialogue;

    public Animator Animator;
    
    
    // Start is called before the first frame update
    void Start()
    {
        Male.clip = WelcomeDialogue;
        Male.Play();
        
        Animator.SetBool("Talking", true);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
