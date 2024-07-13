using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectil : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Rigidbody2D rigidbody2D = other.gameObject.GetComponent<Rigidbody2D>();
            rigidbody2D.AddForce(new Vector2(1,1) * 1000);
        }
        Destroy(this.gameObject);
    }
}
