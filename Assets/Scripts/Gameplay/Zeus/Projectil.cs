using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectil : MonoBehaviour
{
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Rigidbody2D rb2d = other.gameObject.GetComponent<Rigidbody2D>();
            rb2d.velocity = Vector2.zero;
            AddForceDirection(rb2d);
            
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

    private void StartKnockUpCorroutine()
    {
        
    }
    
}
