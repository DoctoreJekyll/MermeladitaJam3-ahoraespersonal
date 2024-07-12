using UnityEngine;
using UnityEngine.InputSystem;

namespace Jugador.NewWaterPlayer
{
    public class PlayerJump : MonoBehaviour
    {
        private Rigidbody2D rb2d;
        private Dash dash;

        [Header("Jump Stuffs")]
        [SerializeField] private float jumpForce;
        [SerializeField] private GameObject pointToCheckFloor;
        [SerializeField] private Vector2 boxCheckSize;
        [SerializeField] private LayerMask floorLayer;
        private bool isJumping;
        public bool isOnFloor;
        private bool canJump;

        [Header("Fall stuffs")]
        public bool isOnAir;
        public Vector2 fallCheck;

        [Header("Coyote Bro")]
        [SerializeField] private float timeToDoCoyote;
        [SerializeField] private float coyoteTime;

        private void Start()
        {
            rb2d = GetComponent<Rigidbody2D>();
            dash = GetComponent<Dash>();
        }

        private void Update()
        {
            CoyoteTimeImprove();
        }

        private void FixedUpdate()
        {
            IsOnFloor();
            FallCheck();
        }

        private void IsOnFloor()
        {
            isOnFloor = Physics2D.OverlapBox(pointToCheckFloor.transform.position, boxCheckSize, 0, floorLayer);
        }

        public void JumpPress()
        {
            if (CanJump())
            {
                JumpMethod();
            }
        }

        public void JumpReleased()
        {
            coyoteTime = 0f;
        }

        private void JumpMethod()
        {
            if (!isOnFloor && !isJumping)
            {
                rb2d.velocity = new Vector2(rb2d.velocity.x, 0f);
                rb2d.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                isJumping = true;
            }
            else
            {
                rb2d.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                isJumping = true;
            }
        }

        private void CoyoteTimeImprove()
        {
            if (isOnFloor)
            {
                coyoteTime = timeToDoCoyote;
            }
            else
            {
                coyoteTime -= Time.deltaTime;
            }
        }

        private void FallCheck()
        {
            if (!isOnFloor)
            {
                isOnAir = true;
                OnAirCalculate();
            }

            if (isOnAir)
            {
                bool overlapBox = Physics2D.OverlapBox(pointToCheckFloor.transform.position, this.fallCheck, 0, floorLayer);
                if (overlapBox)
                {
                    isOnAir = false;
                    isJumping = false;
                }
            }
        }

        private void OnAirCalculate()
        {
            // Lógica adicional si es necesaria para el cálculo en el aire
        }

        private bool CanJump()
        {
            return coyoteTime > 0f && !dash.isDashing && (GamepadButtonSouthIsPush() || KeyBoardButtonSpaceIsPush());
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawCube(pointToCheckFloor.transform.position, fallCheck);
        }

        private bool GamepadButtonSouthIsPush()
        {
            return Gamepad.current != null && Gamepad.current.buttonSouth.isPressed;
        }

        private bool KeyBoardButtonSpaceIsPush()
        {
            return Keyboard.current != null && Keyboard.current.spaceKey.isPressed;
        }
    }
}


