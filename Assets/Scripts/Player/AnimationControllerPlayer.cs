using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationControllerPlayer : MonoBehaviour
{

    [SerializeField] private InputDirection direction;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (direction.Direction().x > 0)
        {
            animator.SetBool("Idle", false);
            animator.SetBool("Run", true);
        }
        else
        {
            animator.SetBool("Idle", true);
            animator.SetBool("Run", false);
        }
    }
}
