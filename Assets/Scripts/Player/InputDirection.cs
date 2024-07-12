using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class InputDirection : MonoBehaviour
{

    [Header("Move")]
    [SerializeField] private InputActionReference inputMove;
    private Vector2 direction;
    
    [Header("Jump")]
    [SerializeField] private InputActionReference inputJump;
    [SerializeField] private UnityEvent jumpPressEvent;
    [SerializeField] private UnityEvent jumpReleaseEvent;
    private bool letJump = false;


    [Header("Dash")] 
    [SerializeField] private InputActionReference inputDash;
    [SerializeField] private UnityEvent dashPressEvent;
    [SerializeField] private Dash dash;

    [Header("Grab")] 
    [SerializeField] private InputActionReference inputGrab;
    [SerializeField] private UnityEvent grabPressEvent;
    [SerializeField] private UnityEvent grabReleaseEvent;
    
    
    public float InputXDirectionValue()
    {
        direction = inputMove.action.ReadValue<Vector2>();
        float xValue = direction.x;
        return xValue;
    }

    public Vector2 Direction()
    {
        return inputMove.action.ReadValue<Vector2>();
    }

    public bool JumpInput()
    {
        return letJump;
    }

    private void OnEnable()
    {
        EnableJump();
        EnableDash();
        EnableGrab();
    }

    private void OnDisable()
    {
        DisableJump();
        DisableDash();
        DisableGrab();
    }

    private void EnableJump()
    {
        inputJump.action.performed += JumpAction;
        inputJump.action.canceled += JumpAction;
    }

    private void DisableJump()
    {
        inputJump.action.performed -= JumpAction;
        inputJump.action.canceled -= JumpAction;
    }

    private void JumpAction(InputAction.CallbackContext obj)
    {
        if (obj.performed)
        {
            if (!dash.isDashing)
            {
                jumpPressEvent.Invoke();
                letJump = true;
            }
        }
        else if (obj.canceled)
        {
            letJump = false;
            jumpReleaseEvent.Invoke();
        }
    }

    private void EnableDash()
    {
        inputDash.action.performed += DashAction;
        inputDash.action.canceled += DashAction;
    }

    private void DisableDash()
    {
        inputDash.action.performed -= DashAction;
        inputDash.action.canceled -= DashAction;
    }

    private void DashAction(InputAction.CallbackContext obj)
    {
        if (obj.performed)
        {
            dashPressEvent.Invoke();
        }
    }

    private void EnableGrab()
    {
        inputGrab.action.performed += GrabAction;
        inputGrab.action.canceled += GrabAction;
    }
    private void DisableGrab()
    {
        inputGrab.action.performed -= GrabAction;
        inputGrab.action.canceled -= GrabAction;
    }

    private void GrabAction(InputAction.CallbackContext obj)
    {
        if (obj.performed)
        {
            grabPressEvent.Invoke();
        }
        else if (obj.canceled)
        {
            grabReleaseEvent.Invoke();
        }
    }


}
