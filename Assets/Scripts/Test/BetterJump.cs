using UnityEngine;
using UnityEngine.InputSystem;

namespace Jugador.NewWaterPlayer
{
    public class BetterJump : MonoBehaviour
    {
        public Rigidbody2D rb;
        [SerializeField] private float fallMultiplier = 2.5f;//Cae más rápido después del salto
        [SerializeField] private float lowJumpMultiplier = 2f;//Salto minimo
        [SerializeField] private float gravityScale;

        [SerializeField] private float upGravity = 2.5f;
        [SerializeField] private float downGravity = 2.5f;

        private PlayerJump playerJump;
        public bool isOnWindArea;
    
        void Start()
        {
            playerJump = GetComponent<PlayerJump>();
            rb = GetComponent<Rigidbody2D>();
            gravityScale = rb.gravityScale;
            isOnWindArea = false;
        }

        private void FixedUpdate()
        {
            if (!isOnWindArea)
            {
                BetterJumpPerformed();
                //JumpGravity();
            }

        }

        private void BetterJumpPerformed()
        {

            if (Gamepad.all.Count > 0)
            {
                if (rb.velocity.y < 0)
                {
                    //rb.velocity += Vector2.up * ((Physics2D.gravity.y * downGravity) * (fallMultiplier - 1) * Time.deltaTime);
                    rb.AddForce(Vector2.up * ((Physics2D.gravity.y * downGravity) * (fallMultiplier - 1) * Time.deltaTime));
                    OnJumpUp();
                }
                else if (rb.velocity.y > 0 && !Gamepad.current.buttonSouth.isPressed && !Keyboard.current.spaceKey.isPressed)
                {
                    //rb.velocity += Vector2.up * ((Physics2D.gravity.y * upGravity) * (lowJumpMultiplier - 1) * Time.deltaTime);
                    rb.AddForce(Vector2.up * ((Physics2D.gravity.y * upGravity) * (lowJumpMultiplier - 1) * Time.deltaTime));
                }
            }
            else
            {
            
                if (rb.velocity.y < 0)
                {
                    //rb.velocity += Vector2.up * ((Physics2D.gravity.y * downGravity) * (fallMultiplier - 1) * Time.deltaTime);
                    rb.AddForce(Vector2.up * ((Physics2D.gravity.y * downGravity) * (fallMultiplier - 1) * Time.deltaTime));
                    OnJumpUp();
                }
                else if (rb.velocity.y > 0 && !Keyboard.current.spaceKey.isPressed)
                {
                    //rb.velocity += Vector2.up * ((Physics2D.gravity.y * upGravity) * (lowJumpMultiplier - 1) * Time.deltaTime);
                    rb.AddForce(Vector2.up * ((Physics2D.gravity.y * upGravity) * (lowJumpMultiplier - 1) * Time.deltaTime));
                }
            }

        }
    
        private void OnJumpUp()
        {
            if (rb.velocity.y > 0)
            {
                rb.AddForce(Vector2.down * (rb.velocity.y * (1- lowJumpMultiplier)), ForceMode2D.Impulse);
            }
        }

        private void JumpGravity()
        {
            if (rb.velocity.y < -0.1f)
            {
                rb.gravityScale = gravityScale * 1.1f;
            }
            else
            {
                rb.gravityScale = gravityScale;
            }
        }
    
    }
}
