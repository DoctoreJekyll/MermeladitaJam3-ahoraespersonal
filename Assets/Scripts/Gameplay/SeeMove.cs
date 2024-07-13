using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SeeMove : MonoBehaviour
{

    [SerializeField] private Rigidbody2D rb2d;
    [SerializeField] private float speed;

    private void OnEnable()
    {
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }
    }

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        rb2d.velocity = Vector2.up * (speed * Time.deltaTime);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            DeadManager.Instance.ODeath();
        }
    }
    
}
