using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class Move : MonoBehaviour
{
    private Rigidbody2D rb;

    [Header("Moves value")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float accelerationValue;
    [SerializeField] private float deccelerationValue;
    [SerializeField] private float velocityPower;
    
    [Header("Input Component")]
    [SerializeField] private InputDirection input;
    private float moveInput;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        moveInput = input.InputXDirectionValue();
    }

    private void FixedUpdate()
    {
       Movement();
    }

    private float TargetSpeed()
    {
        return moveInput * moveSpeed;
    }

    private float SpeedDifferent()
    {
        return TargetSpeed() - rb.velocity.x;
    }

    private float SetAccelerationRate()
    {
        return (Mathf.Abs(TargetSpeed()) > 0.01f) ? accelerationValue : deccelerationValue;
    }

    private float SetFinalMoveValue()
    {
        return Mathf.Pow(Mathf.Abs(SpeedDifferent()) * SetAccelerationRate(), velocityPower) * Mathf.Sign(SpeedDifferent());
    }

    private void Movement()
    {
        rb.AddForce(SetFinalMoveValue() * Vector2.right);
    }

}
