using System;
using System.Collections;
using Jugador.NewWaterPlayer;
using UnityEngine;

public class WallGrab : MonoBehaviour
{
    [Header("Configuration")]
    public bool isOnRightWall;
    public bool isOnLeftWall;
    [SerializeField] private bool onWall;
    [SerializeField] private Vector2 rigthOffset;
    [SerializeField] private Vector2 leftOffset;
    [SerializeField] private float collisionRaidus;
    [SerializeField] private LayerMask groundLayer;

    [Header("Climb values")] 
    [SerializeField] private float slideSpeed;

    [Header("Extra Components")] 
    [SerializeField] private Rigidbody2D rb2d;
    [SerializeField] private ImproveJump improveJump;
    [SerializeField] private Move move;
    [SerializeField] private InputDirection direction;

    private float actualGravityScale;

    private void Start()
    {
        actualGravityScale = rb2d.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        isOnRightWall = Physics2D.OverlapCircle((Vector2)transform.position + rigthOffset, collisionRaidus, groundLayer);
        isOnLeftWall = Physics2D.OverlapCircle((Vector2)transform.position + leftOffset, collisionRaidus, groundLayer);
        onWall = isOnRightWall || isOnLeftWall;

        if (onWall && !grab)
        {
            improveJump.enabled = false;
            WallSlide();
        }
        else
        {
            improveJump.enabled = true;
        }
        
        Grab();

        if (onWall && grab)
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, direction.InputYDirectionValue());
        }

    }

    private bool grab;
    public void GrabInputPut()
    {
        grab = true;
    }

    public void GranInputRelease()
    {
        grab = false;
    }

    public bool isOnGrab;
    private void Grab()
    {
        if (onWall && grab)
        {
            move.enabled = false;
            improveJump.enabled = false;
            rb2d.velocity = new Vector2(rb2d.velocity.x, 0f);
            rb2d.gravityScale = 0;
            isOnGrab = true;
        }
        else
        {
            rb2d.gravityScale = actualGravityScale;
            isOnGrab = false;
            StartCoroutine(ReturnValuesCorroutine());
        }
    }

    private IEnumerator ReturnValuesCorroutine()
    {
        yield return new WaitForSeconds(0.1f);
        move.enabled = true;
        improveJump.enabled = true;
    }

    private void WallSlide()
    {
        rb2d.velocity = new Vector2(rb2d.velocity.x, -slideSpeed);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        
        Gizmos.DrawWireSphere((Vector2)transform.position + rigthOffset, collisionRaidus);
        Gizmos.DrawWireSphere((Vector2)transform.position + leftOffset, collisionRaidus);
    }
}
