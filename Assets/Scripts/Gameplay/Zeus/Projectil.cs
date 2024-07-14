using System;
using System.Collections;
using System.Collections.Generic;
using Jugador.NewWaterPlayer;
using UnityEngine;

public class Projectil : MonoBehaviour
{
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StopAllCoroutines();
            StartCoroutine(StartKnockUpCorroutine(other));
            Destroy(this.gameObject);
        }
    }

    private void AddForceDirection(Rigidbody2D rb2d)
    {
        Flip flip = FindObjectOfType<Flip>();

        if (flip.facingRight)
        {
            rb2d.AddForce(new Vector2(0.5f,0.5f) * 1000);
        }
        else
        {
            rb2d.AddForce(new Vector2(-0.5f,0.5f) * 1000);
        }
    }

    private IEnumerator StartKnockUpCorroutine(Collider2D collider2D)
    {
        Rigidbody2D rb2d = collider2D.gameObject.GetComponent<Rigidbody2D>();
        PlayerJump playerJump = collider2D.gameObject.GetComponent<PlayerJump>();
        ImproveJump improve = collider2D.gameObject.GetComponent<ImproveJump>();
        Dash dash = collider2D.gameObject.GetComponent<Dash>();
        WallGrab wallGrab = collider2D.gameObject.GetComponent<WallGrab>();

        playerJump.enabled = false;
        improve.enabled = false;
        dash.enabled = false;
        wallGrab.enabled = false;
        
        rb2d.velocity = Vector2.zero;
        AddForceDirection(rb2d);

        yield return new WaitForSeconds(0.75f);
        
        playerJump.enabled = true;
        improve.enabled = true;
        dash.enabled = true;
        wallGrab.enabled = true;
        
    }
    
}
