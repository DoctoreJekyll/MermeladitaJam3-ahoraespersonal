using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{

    [SerializeField] private Rigidbody2D rb2d;
    
    private void Update()
    {
        float xRaw = Input.GetAxisRaw("Horizontal");
        float yRaw = Input.GetAxisRaw("Vertical");
        
        if (Input.GetKeyDown(KeyCode.G))
        {
            if(xRaw != 0 || yRaw != 0)
                Dashing(xRaw, yRaw);
        }
    }

    [Header("dash")]
    [SerializeField]private float dashSpeed;
    private void Dashing(float x, float y)
    {
        rb2d.velocity = Vector2.zero;
        Vector2 dir = new Vector2(x, y);

        rb2d.velocity += dir.normalized * dashSpeed;
    }
}
