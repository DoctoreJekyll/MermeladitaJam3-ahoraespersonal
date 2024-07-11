using UnityEngine;
using UnityEngine.InputSystem;

namespace Jugador.NewWaterPlayer
{
    public class PlayerJump : MonoBehaviour
    {

        private Rigidbody2D rb2d;

        [Header("Jump Stuffs")]
        [SerializeField] private float jumpForce;
        [SerializeField] private GameObject pointToCheckFloor;
        [SerializeField] private Vector2 boxCheckSize;
        [SerializeField] private LayerMask floorLayer;
        private bool isJumping;
        public bool isOnFloor;
        private bool canJump;

        [Header("Fall Suffs")]
        public bool isOnAir;
        public Vector2 fallCheck;

        [Header("Coyote Bro")]
        [SerializeField]private float timeToDoCoyote;
        [field: SerializeField]
        public float CoyoteTime { get; private set; }

        [Header("Particles")]
        [SerializeField] private ParticleSystem fallParticle;
        [SerializeField] private ParticleSystem waterFallParticle;

        private void Start()
        {
            rb2d = GetComponent<Rigidbody2D>();
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
        

        public void JumpAction(InputAction.CallbackContext context)//Llamamos a este metodo dentro del componente input action del playermanager 
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

        [Header("Test velocity control on wind")]
        [SerializeField] private float fallMaxVelocity;
        
        private void JumpMethod()//En el primer if detecto si no está en el suelo para saltar pero por el coyote time, aunque no ests en el suelo tienes una ventana para saltar
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

        private void CoyoteTimeImprove()//Control del tiempo para generar el efecto coyote
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

        private void FallCheck()//Método para hacer el check al tocar el suelo
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
                    waterFallParticle.Play();
                    timeTemp = 0;
                }
                else
                {
                    fallParticle.Play();
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
