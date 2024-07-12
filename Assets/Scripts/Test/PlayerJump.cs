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
        [SerializeField]private float timeToDoCoyote;
        [field: SerializeField]
        public float coyoteTime { get; private set; }

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
            if (coyoteTime > 0f && dash.backFromDash)
            {
                JumpMethod();
            }
        }

        public void JumpReleased()
        {
            coyoteTime = 0f;
        }
        
        [SerializeField] private float fallMaxVelocity;
        
        private void JumpMethod()
        {
            Debug.Log("jumpmethod");
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
