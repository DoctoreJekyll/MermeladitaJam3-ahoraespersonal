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

        private void Start()
        {
            rb2d = GetComponent<Rigidbody2D>();
            dash = GetComponent<Dash>();
            grab = GetComponent<WallGrab>();
        }

        public bool canJumpTest;
        private void Update()
        {
            CoyoteTimeImprove();
            canJumpTest = CanJump();
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
            
            if (grab.isOnGrab)
            {
                WallJump();
            }
            
            if (CanJump())
            {
                JumpMethod();
            }
        }
        
        [Header("Wall Jump Settings")]
        [SerializeField] private float wallJumpForce = 10f;
        [SerializeField] private Vector2 wallJumpDirection = new Vector2(1f, 1.5f);
        
        private void WallJump()
        {
            Debug.Log("wall jump working");
    
            // Calculamos la dirección del salto basándonos en la posición de la pared
            Vector2 jumpDirection = new Vector2(
                wallJumpDirection.x * (grab.isOnRightWall ? -1 : 1),
                wallJumpDirection.y
            ).normalized;
    
            Debug.Log($"Jump Direction: {jumpDirection}");
    
            // Ajustamos la velocidad del jugador y añadimos la fuerza del salto
            rb2d.velocity = Vector2.zero;
            rb2d.AddForce(jumpDirection * wallJumpForce, ForceMode2D.Impulse);
    
            isJumping = true;
            grab.GranInputRelease();
        }

        public void JumpReleased()
        {
            coyoteTime = 0f;
        }

        private void JumpMethod()
        {
            if ((!isOnFloor && !isJumping))
            {
                Debug.Log("jump if");
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
                Debug.Log("jump no if");
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
            //Setear la caida aqui para que no sea superrapida
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


