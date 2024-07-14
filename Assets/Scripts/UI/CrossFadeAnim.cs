using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossFadeAnim : MonoBehaviour
{

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
