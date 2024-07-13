using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flip : MonoBehaviour
{

    public bool facingRight;
    private Transform characterTransform;
    [SerializeField] private InputDirection direction;

    void Start()
    {
        characterTransform = GetComponent<Transform>();
    }

    void Update()
    {
        float moveInput = direction.Direction().x;

        if (moveInput > 0 && !facingRight)
        {
            FlipFunction();
        }
        else if (moveInput < 0 && facingRight)
        {
            FlipFunction();
        }
    }

    void FlipFunction()
    {
        facingRight = !facingRight;
        Vector3 scale = characterTransform.localScale;
        scale.x *= -1;
        characterTransform.localScale = scale;
    }
}
