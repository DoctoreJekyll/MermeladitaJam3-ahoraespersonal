
using System.Collections;
using DG.Tweening;
using Jugador.NewWaterPlayer;
using UnityEngine;
using UnityEngine.InputSystem;

public class Dash : MonoBehaviour
{
    private Rigidbody2D rb2d;
    [SerializeField] private InputDirection inputDirection;
    [SerializeField] private PlayerJump playerJump;
    [SerializeField] private Transform pointToCheckFloor;
    [SerializeField] private Vector2 checkSize;
    [SerializeField] private LayerMask layerMask;

    [Header("Dash values")]
    [SerializeField] private float dashForce;
    private bool canDash;
    public bool isDashing; // Ahora público para que PlayerJump lo pueda acceder
    private float dashWindow;

    private void Start()
    {
        camera1 = Camera.main;
        rb2d = GetComponent<Rigidbody2D>();
    }

    public void DashPressed()
    {
        if (!buttonIsPushed)
        {
            if (canDash)
            {
                Dashing(inputDirection.Direction());
            }
        }
    }

    [Header("button test")]
    public bool buttonIsPushed;
    
    private bool GamepadButtonSouthIsPush()
    {
        return Gamepad.current != null && Gamepad.current.buttonSouth.isPressed;
    }

    private bool KeyBoardButtonSpaceIsPush()
    {
        return Keyboard.current != null && Keyboard.current.spaceKey.isPressed;
    }

    private void Update()
    {
        if (GamepadButtonSouthIsPush() || KeyBoardButtonSpaceIsPush())
        {
            buttonIsPushed = true;
        }
        else
        {
            buttonIsPushed = false;
        }
        
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
        StartCoroutine(WitchTime());
        isDashing = true;
        canDash = false;
        rb2d.velocity = Vector2.zero;
        Vector2 dir = direction;

        rb2d.velocity += dir.normalized * dashForce;
    }

    private IEnumerator WitchTime()
    {
        Camera.main.transform.DOComplete();
        camera1.transform.DOShakePosition(.2f, .5f, 14, 90, false, true);
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(0.05f);
        Time.timeScale = 1;
    }

    public float timeDashing;
    public float maxtimeDash;
    private Camera camera1;

    private void DashTimeCount()
    {
        if (isDashing)
        {
            timeDashing += Time.deltaTime;
            if (timeDashing > maxtimeDash)
            {
                isDashing = false; // Finaliza el dash
                timeDashing = 0;
            }
        }
    }

    private void CheckIfFallFromDash()
    {
        if (isDashing)
        {
            bool returnToDash = Physics2D.OverlapBox(pointToCheckFloor.position, checkSize, 0, layerMask);

            if (returnToDash)
            {
                isDashing = false; // Finaliza el dash
            }
        }
    }
}
