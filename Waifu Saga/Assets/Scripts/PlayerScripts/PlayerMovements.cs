using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovements : MonoBehaviour
{
    [Header("Basic Movements")]
    public float speed;
    public float jumpPower;
    public int extraJumps;
    private int jumpCounter;
    private Rigidbody2D body;
    private BoxCollider2D boxCollider;
    private float horizontalMoveInput;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask groundLayer;
    private bool isTouchingGround;

    [Header("Dashing")]
    public float dashSpeed;
    public float dashTime;
    public float dashCooldown;
    private Vector2 dashDirection;
    private bool isDashing;
    private bool canDash = true;
    private bool dashReady = true;
    private bool dashInput;

    // Start is called before the first frame update
    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
    }
    // Update is called once per frame
    void Update()
    {
        // Horizontal movement
        horizontalMoveInput = Input.GetAxis("Horizontal");
        if (horizontalMoveInput > 0.01f)
        {
            transform.localScale = new Vector2(5, 5);
        }
        else if (horizontalMoveInput < -0.01f)
        {
            transform.localScale = new Vector2(-5, 5);
        }
        body.velocity = new Vector2(horizontalMoveInput * speed, body.velocity.y);



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
                Jump();
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



        // Dashing
        dashInput = Input.GetButtonDown("Dash");
        if (dashInput && canDash && dashReady) // If can dash with input
        {
            isDashing = true;
            canDash = false;
            dashReady = false;
            dashDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            if (dashDirection == Vector2.zero) // If no input
            {
                dashDirection = new Vector2(transform.localScale.x, 0f);
            }
            StartCoroutine(stopDash());
        }
        if (isDashing) // Dashing
        {
            body.velocity = dashDirection.normalized * dashSpeed;
            return;
        }
        if (canDash == false) // Reset canDash if on the ground
        {
            if (isTouchingGround)
            {
                canDash = true;
                StartCoroutine(dashOnCooldown());
            }
        }
        IEnumerator stopDash() //Coroutine to time the dash
        {
            yield return new WaitForSeconds(dashTime);
            isDashing = false;
        }
        IEnumerator dashOnCooldown() //Coroutine to dash cooldown
        {
            yield return new WaitForSeconds(dashCooldown);
            dashReady = true;
        }



    }

    void Jump()
    {
        body.velocity = new Vector2(body.velocity.x, jumpPower);
    }
}