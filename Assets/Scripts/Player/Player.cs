using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    #region Variables
    public FiniteStateMachine StateMachine { get; private set; }

    #region Unity Variables
    private PlayerInput playerInput;
    public PlayerInputHandler InputHandler { get; private set; }
    private Camera cam;
    public Rigidbody2D RB { get; private set; }
    public CircleCollider2D CutCollider { get; private set; }
    public Animator Anim { get; private set; }
    [SerializeField] GameObject cutEffect;
    #endregion

    #region State Variables
    public JumpState PlayerJumpState { get; private set; }
    public WallState PlayerWallState { get; private set; }

    #endregion

    public int NormInputY { get; private set; }

    private float jumpInputStartTime;
    private Vector3 jumpStartPosition;
    private Vector2 rawJumpInput;
    private Vector2 jumpDirectionInput;
    private int facingDirection;
    private Vector2 workSpace;
    public Vector2 CurrentVelocty;
    public Vector2 RawMovementInput { get; private set; }
    private Vector2 jumpToPosition;
    private SpriteRenderer sr;
    public bool JumpInput { get; private set; }
    public bool JumpInputStop { get; private set; }
    public bool isTouchingWall;
    [SerializeField] private PlayerData playerData;
    [SerializeField] private LayerMask whatIsGround;
    #endregion

    #region Unity Callbacks

    private void Awake()
    {
        StateMachine = new FiniteStateMachine();

        PlayerWallState = new WallState(this, StateMachine, playerData);
        PlayerJumpState = new JumpState(this, StateMachine, playerData);
    }
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        CutCollider = GetComponent<CircleCollider2D>();
        InputHandler = GetComponent<PlayerInputHandler>();
        RB = GetComponent<Rigidbody2D>();
        Anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        cam = Camera.main;
        JumpInput = false;

        facingDirection = 1;

        StateMachine.Initialize(PlayerWallState);
    }

    // Update is called once per frame
    void Update()
    {
        CheckIfReachedOtherWall();
        StateMachine.CurrentState.LogicUpdate();

    }

    private void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damage();
        /*float dot = CheckJumpStartPosition();
        float angle = Mathf.Acos(dot);
        if (facingDirection < 0)
        {
            angle = (angle * Mathf.Rad2Deg);
        }
        else
        {
            angle = angle * Mathf.Rad2Deg + 180;
        }

        if (collision.name == "TempEnemy")
        {
            SpriteRenderer enemySprite = collision.GetComponent<SpriteRenderer>();  
            GameObject cut1 = Instantiate(cutEffect, transform.position, Quaternion.Euler(0, 0, angle));
            Destroy(cut1, 0.05f);
        }*/
        
    }

    #endregion


    public void SetJumpStart() //Called on entering the jump state.
    {
        jumpStartPosition = transform.position;
    }

    #region Velocity Function
    public void SetJumpVelocity(Vector2 jumpToPos)
    {
        workSpace = jumpToPos*playerData.jumpSpeed;
        //RB.velocity = workSpace;
        //Testing for only jumping in the X
        RB.velocity = new Vector2(workSpace.x, 0);
        CurrentVelocty = workSpace;
    }

    public void SetVelocityY(float newYVelocity)
    {
        workSpace.Set(0, newYVelocity*playerData.moveSpeed);
        RB.velocity = workSpace;
        CurrentVelocty = workSpace;
    }

    public void SetVelocityZero()
    {
        RB.velocity = Vector2.zero;
        CurrentVelocty = RB.velocity;
    }
    #endregion

    #region CheckFunctions

    float CheckJumpStartPosition()
    {
        Vector2 startPos = new Vector2(jumpStartPosition.normalized.x, jumpStartPosition.normalized.y);
        if ((startPos.x < 0 && startPos.y > 0) || (startPos.x > 0 && startPos.y < 0))
        {
            return Vector2.Dot(InputHandler.JumpDirectionInput * facingDirection, new Vector2(-1, 0));
        }
        return Vector2.Dot(InputHandler.JumpDirectionInput * facingDirection, new Vector2(1, 0));
    }
    public void CheckIfReachedOtherWall()
    {
        if(CheckIfFrontTouchingWall())
        {
            Flip();
            SetVelocityZero();
        }
    }

    public bool CheckIfBackTouchingWall()
    {
        return Physics2D.Raycast(transform.position, Vector2.left*facingDirection, playerData.backWallCheckDistance, whatIsGround);
    }

    public bool CheckIfFrontTouchingWall()
    {
        SetVelocityZero();
        return Physics2D.Raycast(transform.position, Vector2.right * facingDirection, playerData.frontWallCheckDistance, whatIsGround);
    }

    public bool CheckIfValidJumpRange()
    {
        if(jumpDirectionInput.y > Mathf.Abs(0.7f))
        {
            InputHandler.UseJumpInput();
            return false;
        }
        return true;
    }

    public void CheckIfShouldFlip()
    {

    }

    #endregion

    public void Damage()
    {
        Debug.Log("I've been hit");
        sr.color = Color.red;
        StartCoroutine(Recolour());
    }

    private IEnumerator Recolour()
    {
        yield return new WaitForSeconds(0.1f);
        sr.color = Color.white;
    }

    public void Flip()
    {
        facingDirection *= -1;
        transform.Rotate(0f, 180f, 0f);
        JumpInput = false; //because we only flip when we need to stop jumping
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, new Vector2(transform.position.x - playerData.backWallCheckDistance, transform.position.y));
        Gizmos.DrawLine(transform.position, new Vector2(transform.position.x + playerData.frontWallCheckDistance, transform.position.y));
    }
}
