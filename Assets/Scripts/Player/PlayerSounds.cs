using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip walk;
    [SerializeField] private AudioClip jump;
    [SerializeField] private AudioClip dash;
    [SerializeField] private AudioClip pick;

    public void SoundEffect()
    {
        audioSource.PlayOneShot(walk);
    }
    
    public void JumpEffect()
    {
        audioSource.PlayOneShot(jump);
    }

    public void DashEffect()
    {
        audioSource.PlayOneShot(dash);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Paper"))
        {
            audioSource.PlayOneShot(pick);
        }
    }
}
