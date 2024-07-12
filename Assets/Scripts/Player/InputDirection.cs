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
    [SerializeField] private bool letJump = false;

    [Header("Dash")] 
    [SerializeField] private InputActionReference dashInput;
    
    
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
        inputJump.action.performed += JumpAction;
        inputJump.action.canceled += JumpAction;
    }

    private void OnDisable()
    {
        inputJump.action.performed -= JumpAction;
        inputJump.action.canceled -= JumpAction;
    }

    private void JumpAction(InputAction.CallbackContext obj)
    {
        if (obj.performed)
        {
            jumpPressEvent.Invoke();
            letJump = true;
        }
        else if (obj.canceled)
        {
            letJump = false;
            jumpReleaseEvent.Invoke();
        }
    }
}
