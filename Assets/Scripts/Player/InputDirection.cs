using UnityEngine;
using UnityEngine.InputSystem;

public class InputDirection : MonoBehaviour
{

    [SerializeField] private InputActionReference inputMove;
    private Vector2 direction;
    
    public float InputXDirectionValue()
    {
        direction = inputMove.action.ReadValue<Vector2>();
        float xValue = direction.x;
        return xValue;
    }


}
