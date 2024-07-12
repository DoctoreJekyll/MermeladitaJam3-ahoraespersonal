
using System;
using Jugador.NewWaterPlayer;
using UnityEngine;

public class Dash : MonoBehaviour
{
    [Header("Components")]
    private Rigidbody2D rb2d;
    [SerializeField] private InputDirection inputDirection;
    [SerializeField] private PlayerJump playerJump;
    [SerializeField] private Transform pointToCheckFloor;
    [SerializeField] private Vector2 checkSize;
    [SerializeField] private LayerMask layerMask;

    [Header("Dash values")]
    [SerializeField] private float dashForce;
    private bool canDash;
    public bool isDashing; // Cambiado a público para acceso desde ImproveJump
    private float dashWindow;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    public void DashPressed()
    {
        if (canDash)
        {
            Dashing(inputDirection.Direction());
        }
    }

    private void Update()
    {
        if (IsOnFloor())
        {
            canDash = true;
        }

        CheckIfFallFromDash();
        DashTimeCount();
    }

    private bool IsOnFloor()
    {
        return playerJump.isOnFloor;
    }

    private void Dashing(Vector2 direction)
    {
        backFromDash = false;
        isDashing = true;
        canDash = false;
        rb2d.velocity = Vector2.zero;
        Vector2 dir = direction;

        rb2d.velocity += dir.normalized * dashForce;
    }

    public float timeDashing;
    public float maxtimeDash;

    private void DashTimeCount()
    {
        if (isDashing)
        {
            timeDashing += Time.deltaTime;
            if (timeDashing > maxtimeDash)
            {
                backFromDash = true;
                isDashing = false; // Dash finalizado
                timeDashing = 0;
            }
        }
    }

    public bool backFromDash;

    private void CheckIfFallFromDash()
    {
        if (isDashing)
        {
            bool returnToDash = Physics2D.OverlapBox(pointToCheckFloor.position, checkSize, 0, layerMask);

            if (returnToDash)
            {
                backFromDash = true;
                isDashing = false; // Dash finalizado
            }
        }
    }
}
