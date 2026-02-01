using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public AudioClip shootSound; 
    private AudioSource audioSource; 
    private Animator animator; 

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>(); 
    }

    void Update()
    {
        
        bool isRunning = animator.GetBool("isRunning");
        bool isDucking = animator.GetBool("isDucking");
        bool isJumping = animator.GetBool("isJumping");
        bool isFalling = animator.GetBool("isFalling");

        
        if (Input.GetMouseButtonDown(0) && !isRunning && !isDucking && !isJumping && !isFalling)
        {
            PlayShootSound();
        }
    }

    void PlayShootSound()
    {
        if (shootSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(shootSound);
        }
    }
}
