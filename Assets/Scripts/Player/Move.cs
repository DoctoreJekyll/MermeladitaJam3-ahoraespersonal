using Jugador.NewWaterPlayer;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Move : MonoBehaviour
{
    private Rigidbody2D rb;
    private PlayerJump playerJump;

    [Header("Moves value")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float accelerationValue;
    [SerializeField] private float deccelerationValue;
    [SerializeField] private float velocityPower;
    
    [Header("Input Component")]
    [SerializeField] private InputDirection input;
    private float moveInput;

    [Header("Animator")] [SerializeField] private Animator animator;

    private void Awake()
    {
        playerJump = GetComponent<PlayerJump>();
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        moveInput = input.InputXDirectionValue();
    }

    private void FixedUpdate()
    {
       Movement();
       if ((input.InputXDirectionValue() > 0 || input.InputXDirectionValue() < 0) && playerJump.isOnFloor)
       {
           animator.Play("PlayerRun");
       }
       else if (playerJump.isOnFloor)
       {
           animator.Play("PlayerIdle");
       }
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
