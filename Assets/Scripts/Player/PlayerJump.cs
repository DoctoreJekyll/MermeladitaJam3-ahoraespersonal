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

        [Header("Grab Component")] private WallGrab grab;
        
        [Header("Animator")] [SerializeField] private Animator animator;

        private void Start()
        {
            animator.GetComponentsInChildren<Animator>();
            rb2d = GetComponent<Rigidbody2D>();
            dash = GetComponent<Dash>();
            grab = GetComponent<WallGrab>();
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
            if (isOnFloor && (grab.isOnRightWall || grab.isOnLeftWall))
            {
                Debug.Log("jump on wall");
                JumpMethod();
            }
            else if (grab.isOnGrab)
            {
                WallJump();
            }
            else if (CanJump())
            {
                JumpMethod();
            }
        }

        public Vector2 wallJumpForce = new Vector2();
        public float forcevale;
        
        //TEMPORAL
        private void WallJump()
        {
            Vector2 force = new Vector2(wallJumpForce.x, wallJumpForce.y);

            int dir = grab.isOnRightWall ? -1 : 1; // -1 para derecha, 1 para izquierda

            force.x *= dir;

            if (rb2d.velocity.y < 0)
                force.y -= rb2d.velocity.y;

            rb2d.AddForce(force * forcevale, ForceMode2D.Impulse);
        }
        
        public void JumpReleased()
        {
            coyoteTime = 0f;
        }

        private void JumpMethod()
        {
            if ((!isOnFloor && !isJumping))
            {
                rb2d.velocity = new Vector2(rb2d.velocity.x, 0f);
                rb2d.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                isJumping = true;
            }
            else if (grab.isOnGrab)
            {
                rb2d.velocity = new Vector2(rb2d.velocity.x, 0f);
                rb2d.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                isJumping = true;
            }
            else
            {
                rb2d.velocity = new Vector2(rb2d.velocity.x, 0f);
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
            else if (grab.isOnGrab)
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
            if (!isOnFloor && grab.isOnGrab)
            {
                isOnAir = false;
                
            }
            else if (!isOnFloor)
            {
                isOnAir = true;
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

        private bool CanJump()
        {
            return coyoteTime > 0f && !dash.isDashing && (isOnFloor || grab.isOnGrab) && (GamepadButtonSouthIsPush() || KeyBoardButtonSpaceIsPush());
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


