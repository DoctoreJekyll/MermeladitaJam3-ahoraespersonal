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

        [Header("Better Jump Gravity")] 
        [SerializeField] private float setNormalGravity = 3f;
        [SerializeField] private float setLimitUpGravity = 10.8f;
        [SerializeField] private float setLaterLimitUpGravity = 9f;
        [SerializeField] private float setRbDrag = 0.25f;
        
        [SerializeField] private float timePushingButton;
        [SerializeField] private float maxTimeToJump;
    
        void Start()
        {
            playerJump = GetComponent<PlayerJump>();
            rb = GetComponent<Rigidbody2D>();
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
            if (Gamepad.current != null)
            {
                if (Gamepad.all.Count > 0)
                {
                    return true;
                }
            }

            return false;
        }
        private bool GamepadButtonSouthIsPush()
        {
            if (Gamepad.current != null)
            {
                return Gamepad.current.buttonSouth.isPressed;
            }

            return false;
        }

        private bool KeyBoardButtonSpaceIsPush() => Keyboard.current.spaceKey.isPressed;
    
    }
}
