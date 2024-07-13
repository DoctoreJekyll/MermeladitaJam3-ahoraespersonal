using System;
using UnityEngine;

public class ClimbEdges : MonoBehaviour
{
    private bool headRay;
    private bool footRay;
    private bool canJumpOver;
    private Flip flip;
    private Rigidbody2D rb2d;

    [SerializeField] private float headOffset;
    [SerializeField] private float footOffset;
    [SerializeField] private float distance;
    [SerializeField] private LayerMask layerWall;

    private void Start()
    {
        flip = GetComponent<Flip>();
        rb2d = GetComponent<Rigidbody2D>();
    }

    // private void FixedUpdate()
    // {
    //     // Cast rays
    //     headRay = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + headOffset), transform.right, distance, layerWall);
    //     footRay = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + footOffset), transform.right, distance, layerWall);
    //
    //     // Apply force if footRay hits and headRay does not
    //     if (footRay && !headRay && !canJumpOver)
    //     {
    //         Vector2 force = flip.facingRight ? new Vector2(0.5f, 0.5f) : new Vector2(-0.5f, 0.5f);
    //         rb2d.AddForce(force * 1000);
    //     }
    // }

    private void Update()
    {
        // Debug rays

    }
    
}

