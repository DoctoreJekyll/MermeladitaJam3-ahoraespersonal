using System.Collections;
using System.Collections.Generic;
using Jugador.NewWaterPlayer;
using UnityEngine;

public class Flip : MonoBehaviour
{

    public bool facingRight;
    private Transform characterTransform;
    [SerializeField] private InputDirection direction;
    [SerializeField] private ParticleSystem dust;

    private PlayerJump playerJump;

    void Start()
    {
        playerJump = GetComponent<PlayerJump>();
        characterTransform = GetComponent<Transform>();
    }

    void Update()
    {
        float moveInput = direction.Direction().x;

        if (moveInput > 0 && !facingRight)
        {
            FlipFunction();
        }
        else if (moveInput < 0 && facingRight)
        {
            FlipFunction();
        }
    }

    void FlipFunction()
    {
        if (playerJump.isOnFloor)
        {
            dust.Play();
        }
        
        facingRight = !facingRight;
        Vector3 scale = characterTransform.localScale;
        scale.x *= -1;
        characterTransform.localScale = scale;
    }
}
