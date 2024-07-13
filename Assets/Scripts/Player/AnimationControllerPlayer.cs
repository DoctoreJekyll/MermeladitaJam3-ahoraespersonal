using System;
using System.Collections;
using System.Collections.Generic;
using Jugador.NewWaterPlayer;
using UnityEngine;

public class AnimationControllerPlayer : MonoBehaviour
{

    [SerializeField] private InputDirection direction;
    [SerializeField] private Rigidbody2D rb2d;
    [SerializeField] private PlayerJump playerJump;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (direction.Direction().x > 0 || direction.Direction().x < 0)
        {
            animator.Play("PlayerRun");
        }
        else
        {
            animator.Play("PlayerIdle");
        }
    }
}
