using System;
using System.Collections;
using System.Collections.Generic;
using Jugador.NewWaterPlayer;
using UnityEngine;

public class Projectil : MonoBehaviour
{

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (rb.velocity.y < -10)
        {
            rb.velocity = new Vector2(rb.velocity.x, -10);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(StartKnockUpCorroutine(other));
        }
    }

    private void AddForceDirection(Rigidbody2D rb2d)
    {
        Flip flip = FindObjectOfType<Flip>();

        if (flip.facingRight)
        {
            rb2d.AddForce(new Vector2(-0.5f,0.5f) * 500);
        }
        else
        {
            rb2d.AddForce(new Vector2(0.5f,0.5f) * 500);
        }
    }

    private IEnumerator StartKnockUpCorroutine(Collider2D collider2D)
    {
        Rigidbody2D rb2d = collider2D.gameObject.GetComponent<Rigidbody2D>();
        ImproveJump improve = collider2D.gameObject.GetComponent<ImproveJump>();
        WallGrab wallGrab = collider2D.gameObject.GetComponent<WallGrab>();
        PlayerJump playerJump = collider2D.gameObject.GetComponent<PlayerJump>();
        
        wallGrab.enabled = false;
        improve.enabled = false;
        playerJump.enabled = false;
        
        rb2d.velocity = Vector2.zero;
        AddForceDirection(rb2d);

        Collider2D coll = GetComponent<Collider2D>();
        coll.enabled = false;

        yield return new WaitForSeconds(0.35f);
        
        wallGrab.enabled = true;
        improve.enabled = true;
        playerJump.enabled = true;
        
        Destroy(this.gameObject, .5f);

    }
    
}
