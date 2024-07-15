using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossFadeAnim : MonoBehaviour
{
    private void OnEnable()
    {
        Time.timeScale = 1;
    }

    [SerializeField] private Animator animator;
    
    public void FadeOut()
    {
        animator.Play("fadeout");
    }

    public void FadeIn()
    {
        animator.Play("fadein");
    }
    
    
}
