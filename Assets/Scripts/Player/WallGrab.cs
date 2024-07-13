using System;
using System.Collections;
using Jugador.NewWaterPlayer;
using UnityEngine;
using UnityEngine.Events;

public class WallGrab : MonoBehaviour
{
    [Header("Configuration")]
    public bool isOnRightWall;
    public bool isOnLeftWall;
    [SerializeField] private bool onWall;
    [SerializeField] private Vector2 rigthOffset;
    [SerializeField] private Vector2 leftOffset;
    [SerializeField] private float collisionRaidus;
    [SerializeField] private LayerMask groundLayer;

    [Header("Climb values")] 
    [SerializeField] private float slideSpeed;
    [SerializeField] private float maxTimeClimb;
    private float timeClimbing;

    [Header("Extra Components")] 
    [SerializeField] private Rigidbody2D rb2d;
    [SerializeField] private ImproveJump improveJump;
    [SerializeField] private Move move;
    [SerializeField] private InputDirection direction;

    private float actualGravityScale;
    private Flip flip;
    private PlayerJump playerJump;
    
    // Eventos personalizados
    public UnityEvent OnRightWallExit;
    public UnityEvent OnLeftWallExit;

    private void Start()
    {
        test = StartCoroutine(TimeCorroutine());
        playerJump = GetComponent<PlayerJump>();
        flip = GetComponent<Flip>();
        actualGravityScale = rb2d.gravityScale;
        
        // Inicializar eventos
        if (OnRightWallExit == null)
            OnRightWallExit = new UnityEvent();

        if (OnLeftWallExit == null)
            OnLeftWallExit = new UnityEvent();

        // Asignar métodos a los eventos (opcional, si quieres hacerlo desde código)
        OnRightWallExit.AddListener(OnExitRightWall);
        OnLeftWallExit.AddListener(OnExitLeftWall);
    }

    public float upForce = 300;
    // Métodos que se llaman cuando los eventos se disparan
    private void OnExitRightWall()
    {
        Debug.Log("Saliendo de la pared derecha");
        // Aquí puedes agregar la lógica que deseas ejecutar
        rb2d.AddForce(Vector2.up * upForce);
        move.enabled = true;
    }

    private void OnExitLeftWall()
    {
        // Aquí puedes agregar la lógica que deseas ejecutar
        rb2d.AddForce(Vector2.up * upForce);
        move.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        bool currentRightWall = Physics2D.OverlapCircle((Vector2)transform.position + rigthOffset, collisionRaidus, groundLayer);
        bool currentLeftWall = Physics2D.OverlapCircle((Vector2)transform.position + leftOffset, collisionRaidus, groundLayer);

        // Detectar cambio de estado en isOnRightWall
        if (isOnRightWall && !currentRightWall)
        {
            if (grab)
            {
                OnRightWallExit.Invoke();
            }
            
        }

        // Detectar cambio de estado en isOnLeftWall
        if (isOnLeftWall && !currentLeftWall)
        {
            if (grab)
            {
                OnLeftWallExit.Invoke();
            }
        }

        // Actualizar estados
        isOnRightWall = currentRightWall;
        isOnLeftWall = currentLeftWall;
        onWall = isOnRightWall || isOnLeftWall;

        if (onWall && !grab)
        {
            improveJump.enabled = false;
            WallSlide();
        }
        else
        {
            improveJump.enabled = true;
        }
        
        Grab();
        
        if (playerJump.isOnFloor)
        {
            StopCoroutine(test);
        }

    }

    public float speed;
    private void FixedUpdate()
    {
        if (onWall && grab)
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, direction.InputYDirectionValue() * speed);
            flip.enabled = false;
        }
        else
        {
            flip.enabled = true;
        }
    }

    private bool grab;
    private Coroutine test;
    public void GrabInputPut()
    {
        //FallFromGrab();
        grab = true;
    }

    private void FallFromGrab()
    {
        test = StartCoroutine(TimeCorroutine());
    }

    public void GranInputRelease()
    {
        grab = false;
    }

    IEnumerator TimeCorroutine()
    {
        yield return new WaitForSeconds(3f);
        grab = false;
    }

    public bool isOnGrab;
    private void Grab()
    {
        if (onWall && grab)
        {
            move.enabled = false;
            improveJump.enabled = false;
            rb2d.velocity = new Vector2(rb2d.velocity.x, 0f);
            rb2d.gravityScale = 0;
            isOnGrab = true;
        }
        else
        {
            rb2d.gravityScale = actualGravityScale;
            isOnGrab = false;
            StartCoroutine(ReturnValuesCorroutine());
        }
    }

    private IEnumerator ReturnValuesCorroutine()
    {
        yield return new WaitForSeconds(0.1f);
        move.enabled = true;
        improveJump.enabled = true;
    }

    private void WallSlide()
    {
        rb2d.velocity = new Vector2(rb2d.velocity.x, -slideSpeed);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        
        Gizmos.DrawWireSphere((Vector2)transform.position + rigthOffset, collisionRaidus);
        Gizmos.DrawWireSphere((Vector2)transform.position + leftOffset, collisionRaidus);
    }
}
