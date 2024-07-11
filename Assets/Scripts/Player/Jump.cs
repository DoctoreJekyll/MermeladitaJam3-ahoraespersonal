using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    private Rigidbody2D rb;

    [Header("Values")] 
    [SerializeField] private float jumpForce;
    [SerializeField] private float jumpCutMultiplier;
    
    [Header("Configuration")]
    [SerializeField] private Transform groundCheckPoint;
    [SerializeField] private Vector2 groundCheckSize;
    [SerializeField] private LayerMask layerGround;
    [SerializeField] private InputDirection inputControl;
    
    [Header("Check")]
    [SerializeField] private bool isOnGroundCheck;
    [SerializeField] private bool isJumping;
    [SerializeField] private bool jumpInputRelease;


    private float normalGravity;
    [SerializeField] private float maxFallSpeed;
    [SerializeField] private bool isFalling;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        normalGravity = rb.gravityScale;
    }

    private void FixedUpdate()
    {
        OnFloor();
        OnJumpUp();
        Falling();

        if (isOnGroundCheck)
        {
            isJumping = false;
        }
    }

    public void JumpFunctionEvent()
    {
        if (isOnGroundCheck && !isJumping)
        {
            Jumping();
        }
    }

    private void OnFloor()
    {
        isOnGroundCheck = Physics2D.OverlapBox(groundCheckPoint.position, groundCheckSize, 0, layerGround);

        if (isOnGroundCheck == false)
        {
            isJumping = true;
        }
    }

    private void Jumping()
    {
        SetGravityScale(normalGravity);
        rb.velocity = new Vector2(rb.velocity.x, 0f);
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        isJumping = true;
    }

    private void Falling()
    {
        if (rb.velocity.y < 0)
        {
            isFalling = true;
        }
    }

    private void OnJumpUp()
    {
        if (!inputControl.JumpInput())
        {
            if (rb.velocity.y > 0 && isJumping)
            {
                Debug.Log("release");
                SetGravityScale(normalGravity * jumpCutMultiplier);
            }
        }
        jumpInputRelease = true;
    }

    private void SetGravityScale(float scale)
    {
        rb.gravityScale = scale;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawCube(groundCheckPoint.position, groundCheckSize);
    }
}
