using UnityEngine;
using UnityEngine.InputSystem;

namespace Jugador.NewWaterPlayer
{
    public class ImproveJump : MonoBehaviour
    {
        [Header("References")]
        private Rigidbody2D rb;
        private PlayerJump playerJump;
        private Dash dashComponent;

        [Header("Configuration")]
        [SerializeField] private float fallMultiplier = 2.5f; // Cae más rápido después del salto
        [SerializeField] private float lowJumpDivide = 2f;
        [SerializeField] private float gravity;
        [SerializeField] private float fallLimit;

        [Header("Better Jump Gravity")]
        [SerializeField] private float setNormalGravity = 3f;
        [SerializeField] private float setLimitUpGravity = 10.8f;
        [SerializeField] private float setLaterLimitUpGravity = 9f;
        [SerializeField] private float setRbDrag = 0.25f;

        [Header("Input Settings")]
        [SerializeField] private float maxTimeToJump = 0.1f; // Tiempo máximo para considerar un salto prolongado

        private float timePushingButton; // Tiempo transcurrido presionando el botón de salto prolongado
        [SerializeField] private float fallMaxVelocity;

        void Start()
        {
            playerJump = GetComponent<PlayerJump>();
            rb = GetComponent<Rigidbody2D>();
            dashComponent = GetComponent<Dash>();
        }

        private void Update()
        {
            ClampUpVelocity();
            ClampDownVelocity();
            
            Debug.Log(rb.velocity.y);
        }

        private void FixedUpdate()
        {
            BetterJumpPerformed();
            
            // Manejar la entrada del botón de salto prolongado
            HandleExtendedJump();

            // Limitar la velocidad de caída
            if (rb.velocity.y < fallLimit)
            {
                rb.velocity = new Vector2(rb.velocity.x, fallLimit);
            }
        }

        private void BetterJumpPerformed()
        {
            if (playerJump.isOnFloor)
            {
                rb.gravityScale = setNormalGravity;
                rb.drag = 0;

                if (rb.gravityScale > setLimitUpGravity)
                {
                    rb.gravityScale = setLaterLimitUpGravity;
                }
            }
            else
            {
                rb.gravityScale = gravity;
                rb.drag = setRbDrag;

                // Aplicar la gravedad modificada según las condiciones de salto
                if (rb.velocity.y < 0)
                {
                    rb.gravityScale = gravity * fallMultiplier;
                }
                else if ((rb.velocity.y > 0 && !IsJumpInputActive()) || timePushingButton > maxTimeToJump)
                {
                    rb.gravityScale = gravity * (fallMultiplier / lowJumpDivide);
                }
            }
        }

        private void HandleExtendedJump()
        {
            // Solo aumentar el tiempo de presión del botón si no se está realizando un dash
            if (!dashComponent.isDashing && (GamepadButtonSouthIsPush() || KeyBoardButtonSpaceIsPush()))
            {
                timePushingButton += Time.deltaTime;
            }
            else
            {
                timePushingButton = 0;
            }
        }

        private bool GamepadButtonSouthIsPush()
        {
            return Gamepad.current != null && Gamepad.current.buttonSouth.isPressed;
        }

        private bool KeyBoardButtonSpaceIsPush()
        {
            return Keyboard.current != null && Keyboard.current.spaceKey.isPressed;
        }

        private bool IsJumpInputActive()
        {
            return GamepadButtonSouthIsPush() || KeyBoardButtonSpaceIsPush();
        }

        private void ClampUpVelocity()
        {
            if (rb.velocity.y > fallMaxVelocity)
                rb.velocity = new Vector2(rb.velocity.x, fallMaxVelocity);
        }

        [SerializeField] private float fallingVelocity;
        private void ClampDownVelocity()
        {
            if (rb.velocity.y < fallingVelocity)
                rb.velocity = new Vector2(rb.velocity.x, fallingVelocity);
        }
    }
}


