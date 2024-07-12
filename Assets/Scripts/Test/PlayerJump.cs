using UnityEngine;
using UnityEngine.InputSystem;

namespace Jugador.NewWaterPlayer
{
    public class PlayerJump : MonoBehaviour
    {

        private Rigidbody2D rb2d;
        [SerializeField] private InputActionReference inputJump;

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
        [SerializeField]private float timeToDoCoyote;
        [field: SerializeField]
        public float CoyoteTime { get; private set; }

        private void Start()
        {
            rb2d = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            CoyoteTimeImprove();
            
            
            float xRaw = Input.GetAxisRaw("Horizontal");
            float yRaw = Input.GetAxisRaw("Vertical");
            
            if (Input.GetKeyDown(KeyCode.G))
            {
                if(xRaw != 0 || yRaw != 0)
                    Dash(xRaw, yRaw);
            }
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
        

        private void JumpAction(InputAction.CallbackContext context)//Llamamos a este metodo dentro del componente input action del playermanager 
        {
            if (context.performed)
            {
                if (CoyoteTime > 0f)
                {
                    JumpMethod();
                }
            }
            
            if (context.canceled)
            {
                CoyoteTime = 0f;
            }
        }

        [Header("dash")]
        [SerializeField]private float dashSpeed;
        private void Dash(float x, float y)
        {
            rb2d.velocity = Vector2.zero;
            Vector2 dir = new Vector2(x, y);

            rb2d.velocity += dir.normalized * dashSpeed;
        }
        
        [SerializeField] private float fallMaxVelocity;
        
        private void JumpMethod()
        {
            if (!isOnFloor && !isJumping)
            {
                rb2d.velocity = new Vector2(rb2d.velocity.x,0f);
                rb2d.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                ClampUpVelocity();
                isJumping = true;
            }
            else
            {
                rb2d.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                ClampUpVelocity();
                isJumping = true;
            }

        }

        private void ClampUpVelocity()
        {
            if (rb2d.velocity.y > fallMaxVelocity)
                rb2d.velocity = new Vector2(rb2d.velocity.x, fallMaxVelocity);
        }

        private void CoyoteTimeImprove()
        {
            if (isOnFloor)
            {
                CoyoteTime = timeToDoCoyote;
            }
            else
            {
                CoyoteTime -= Time.deltaTime;
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
                    FallingParticle();
                    isOnAir = false;
                    isJumping = false;
                }

            }
        }

        float timeTemp = 0;
        private void OnAirCalculate()
        {
            if (isOnAir)
            {
                timeTemp += Time.deltaTime;
            }
        }
        private void FallingParticle()
        {
            if (isOnAir)
            {
                if (timeTemp >= 1f)
                {
                    //waterFallParticle.Play();
                    timeTemp = 0;
                }
                else
                {
                    //fallParticle.Play();
                    timeTemp = 0;
                }
            }
        }
        
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawCube(pointToCheckFloor.transform.position, fallCheck);
        }
    
    }
}
