using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XInput;

namespace Jugador.NewWaterPlayer
{
    public class ImproveJump : MonoBehaviour
    {
        [Header("References")]
        private Rigidbody2D rb;
        private PlayerJump playerJump;
    
        [Header("Configuration")]
        [SerializeField] private float fallMultiplier = 2.5f;//Cae más rápido después del salto
        [SerializeField] private float lowJumpDivide = 2f;
        [SerializeField] private float gravity;
        [SerializeField] private float fallLimit;
    
        public bool isOnWindArea;
        public bool isInEspecialTile;
        [SerializeField] private float timePushingButton;
        [SerializeField] private float maxTimeToJump;
    
        void Start()
        {
            playerJump = GetComponent<PlayerJump>();
            rb = GetComponent<Rigidbody2D>();
            isOnWindArea = false;
        }

        [Header("Test wind velocity")] 
        [SerializeField] private float fallMaxVelocity;
        private void Update()
        {
            ClampUpVelocity();
        }

        private void FixedUpdate()
        {
            BetterJumpPerformed();
            if (GamepadButtonSouthIsPush() || KeyBoardButtonSpaceIsPush())
            {
                timePushingButton += Time.deltaTime;
            }
            else
            {
                timePushingButton = 0;
            }
        
            if (rb.velocity.y < fallLimit)
            {
                rb.velocity = new Vector2(rb.velocity.x, fallLimit);
            }
        }

        private void BetterJumpPerformed()
        {
            if (playerJump.isOnFloor)
            {
                rb.gravityScale = 3f;
                rb.drag = 0;

                if (rb.gravityScale > 10.8)
                {
                    rb.gravityScale = 9;
                }
            }
            else
            {
                rb.gravityScale = gravity;
                rb.drag = 0.25f;
                if (GamepadIsConnected())
                {
                    if (rb.velocity.y < 0)
                    {
                        rb.gravityScale = gravity * fallMultiplier;
                    }
                    else if ((rb.velocity.y > 0 && !GamepadButtonSouthIsPush() && !KeyBoardButtonSpaceIsPush()) || timePushingButton > maxTimeToJump)
                    {
                        rb.gravityScale = gravity * (fallMultiplier / lowJumpDivide);
                    }
                }
                else
                {
                    if (rb.velocity.y < 0)
                    {
                        rb.gravityScale = gravity * fallMultiplier;
                    }
                    else if ((rb.velocity.y > 0 && !KeyBoardButtonSpaceIsPush()) || timePushingButton > maxTimeToJump)
                    {
                        rb.gravityScale = gravity * (fallMultiplier / lowJumpDivide);
                    }
                }
            }
        }
        
        private void ClampUpVelocity()
        {
            if (rb.velocity.y > fallMaxVelocity)
                rb.velocity = new Vector2(rb.velocity.x, fallMaxVelocity);
        }

        private bool GamepadIsConnected()
        {
            if (XInputController.current != null)
            {
                if (XInputController.all.Count > 0)
                {
                    return true;
                }
            }

            return false;
        }
        private bool GamepadButtonSouthIsPush()
        {
            if (XInputController.current != null)
            {
                return XInputController.current.buttonSouth.isPressed;
            }

            return false;
        }

        private bool KeyBoardButtonSpaceIsPush() => Keyboard.current.spaceKey.isPressed;
    
    }
}
