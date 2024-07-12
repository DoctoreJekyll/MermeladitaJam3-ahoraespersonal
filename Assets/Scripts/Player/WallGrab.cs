using Jugador.NewWaterPlayer;
using UnityEngine;

public class WallGrab : MonoBehaviour
{
    [Header("Configuration")]
    [SerializeField] private bool onWall;
    [SerializeField] private Vector2 rigthOffset;
    [SerializeField] private Vector2 leftOffset;
    [SerializeField] private float collisionRaidus;
    [SerializeField] private LayerMask groundLayer;

    [Header("Extra Components")] 
    [SerializeField] private Rigidbody2D rb2d;
    [SerializeField] private ImproveJump improveJump;
    
    // Update is called once per frame
    void Update()
    {
        onWall = Physics2D.OverlapCircle((Vector2)transform.position + rigthOffset, collisionRaidus, groundLayer)
            || Physics2D.OverlapCircle((Vector2)transform.position + leftOffset, collisionRaidus, groundLayer);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        
        Gizmos.DrawWireSphere((Vector2)transform.position + rigthOffset, collisionRaidus);
        Gizmos.DrawWireSphere((Vector2)transform.position + leftOffset, collisionRaidus);
    }
}
