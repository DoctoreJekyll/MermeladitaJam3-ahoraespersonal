using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip walk;
    [SerializeField] private AudioClip jump;
    [SerializeField] private AudioClip dash;

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
}
