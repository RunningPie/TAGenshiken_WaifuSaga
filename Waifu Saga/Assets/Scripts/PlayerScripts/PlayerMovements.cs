using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovements : MonoBehaviour
{
    private Rigidbody2D body;
    private BoxCollider2D boxCollider;
    public Camera mainCam;

    private Animator anim;

    [Header("Basic Movements")]
    public float speed;
    public float jumpPower;
    public int extraJumps;
    [SerializeField] private int jumpCounter;
    private float horizontalMoveInput;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask groundLayer;
    [SerializeField] private bool isTouchingGround;

    [Header("Dashing")]
    public float dashSpeed;
    public float dashTime;
    public float dashCooldown;
    public float airTimeDivider;
    public bool dashToMouse;
    public Transform dashTarget;
    private Vector2 dashDirection;
    [SerializeField] private bool isDashing;
    [SerializeField] private bool canDash = true;
    [SerializeField] private bool dashReady = true;
    private bool dashInput;

    [Header("Wall Jump")]
    public Transform wallCheck;
    public float wallSlideSpeed;
    public bool isWallTouch;
    public bool isWallSlide;
    public float wallJumpTime;
    public Vector2 wallJumpPower;
    private bool isWallJumping;
    

    [Header("Level Loading")]
    // public GameObject[] players;
    public static PlayerMovements Instance;

    // Start is called before the first frame update
    void Awake()
    {
        // Grab References
        body = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
    }

    void Start()
    {
        if (Instance != null)
        {
            Destroy(this.gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void OnLevelWasLoaded(int level)
    {
        FindStartPos();

    //     players = GameObject.FindGameObjectsWithTag("Player");

    //     if (players.Length > 1)
    //     {
    //         Destroy(players[1]);
    //     }
    }

    void FindStartPos()
    {
        transform.position = GameObject.FindWithTag("startPos").transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Horizontal movement
        horizontalMoveInput = Input.GetAxis("Horizontal");
        if (horizontalMoveInput > 0.01f)
        {
            transform.localScale = new Vector2(1f, 1f);
        }
        else if (horizontalMoveInput < -0.01f)
        {
            transform.localScale = new Vector2(-1f, 1f);
        }
        body.velocity = new Vector2(horizontalMoveInput * speed, body.velocity.y);
        //Animation
        anim.SetBool("Run", horizontalMoveInput != 0);

        

        //Jumping
        isTouchingGround = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        if (isTouchingGround) // Reset jump counter every time player lands
        {
            jumpCounter = extraJumps;
        }
        if(Input.GetButtonDown("Jump")) // Check jump input
        {
            if(isTouchingGround) // Check if player is on the ground
            {
                // Jump animation
                Jump();
            }
            else if(isWallSlide)
            {
                // Wall jump animation
                isWallJumping = true;
                jumpCounter--;
                StartCoroutine(stopWallJump());
            }
            else
            {
                if (jumpCounter > 0) // Check if jump counter above zero
                {
                    Jump();
                    jumpCounter--;
                }
            }
        }   
        void Jump()
        {
            body.velocity = new Vector2(body.velocity.x, jumpPower);
        } 



        // Dashing
        dashInput = Input.GetButtonDown("Dash");
        if (dashInput && canDash && dashReady) // If can dash with input
        {
            // Dash animation
            isDashing = true;
            canDash = false;
            dashReady = false;
            if (dashToMouse)
            {
                dashDirection = (dashTarget.position - transform.position).normalized;
            }
            else if (dashToMouse == false)
            {
                dashDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
            }
            if (dashDirection == Vector2.zero) // If no input
            {
                dashDirection = new Vector2(transform.localScale.x, 0f)/5f;
            }
            StartCoroutine(stopDash());
        }
        if (isDashing) // Dashing
        {
            body.velocity = dashDirection * dashSpeed;
            return;
        }
        if (isTouchingGround)
        {
            canDash = true;
        }
        IEnumerator dashOnCooldown() //Coroutine to dash cooldown
        {
            yield return new WaitForSeconds(dashCooldown);
            dashReady = true;
        }
        IEnumerator stopDash() //Coroutine to time the dash
        {
            yield return new WaitForSeconds(dashTime);
            isDashing = false;
            body.velocity = body.velocity / airTimeDivider;
            StartCoroutine(dashOnCooldown());
        }



        // Wall slide and jump
        isWallTouch = Physics2D.OverlapBox(wallCheck.position, new Vector2(0.02f, 0.25f), 0, groundLayer);
        if(isWallTouch && !isTouchingGround)
        {
            // Wall hang idle animation
            isWallSlide = true;
            canDash = true;
        }
        else
        {
            // Idle animation
            isWallSlide = false;
        }
        if(isWallSlide)
        {
            body.velocity = new Vector2(body.velocity.x, Mathf.Clamp(body.velocity.y, -(wallSlideSpeed), float.MaxValue));
            jumpCounter = extraJumps;
        }
        if(isWallJumping)
        {
            body.velocity = new Vector2(-horizontalMoveInput * wallJumpPower.x, wallJumpPower.y);
        }
        else
        {
            body.velocity = new Vector2(horizontalMoveInput * speed, body.velocity.y);
        }
        IEnumerator stopWallJump()
        {
            yield return new WaitForSeconds(wallJumpTime);
            isWallJumping = false;
        }
    }
}