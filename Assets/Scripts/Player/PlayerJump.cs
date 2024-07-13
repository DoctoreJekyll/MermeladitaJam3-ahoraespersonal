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
                Debug.Log("walljump");
                WallJump();
            }
            
            if (CanJump())
            {
                JumpMethod();
            }
        }

        public Vector2 wallJumpForce = new Vector2();
        public float forcevale;
        private void WallJump()
        {
            // Determina la dirección del salto (izquierda o derecha)
            int dir = grab.isOnRightWall ? -1 : 1; // -1 para derecha, 1 para izquierda

            // Calcula la fuerza de salto
            Vector2 jumpForce = new Vector2(wallJumpForce.x * dir, wallJumpForce.y);

            // Asegura que el jugador alcance la fuerza de salto deseada o mayor
            if (rb2d.velocity.y < 0)
            {
                jumpForce.y = Mathf.Max(jumpForce.y, -rb2d.velocity.y);
            }

            // Aplica la fuerza como un impulso para garantizar que se aplique instantáneamente
            rb2d.AddForce(jumpForce * forcevale, ForceMode2D.Impulse);
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


