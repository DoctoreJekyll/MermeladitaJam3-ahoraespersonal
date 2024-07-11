using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class InputDirection : MonoBehaviour
{

    [SerializeField] private InputActionReference inputMove;
    [SerializeField] private InputActionReference inputJump;
    [SerializeField] private UnityEvent jumpEvent;
    [SerializeField] private bool letJump = false;
    private Vector2 direction;
    
    public float InputXDirectionValue()
    {
        direction = inputMove.action.ReadValue<Vector2>();
        float xValue = direction.x;
        return xValue;
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
            jumpEvent.Invoke();
            letJump = true;
            Debug.Log("press");
        }
        else if (obj.canceled)
        {
            letJump = false;
            Debug.Log("unpress");
        }
    }
}
