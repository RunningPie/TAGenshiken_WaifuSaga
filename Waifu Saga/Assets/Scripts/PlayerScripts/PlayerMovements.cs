using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovements : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;
    private Rigidbody2D body;
    private BoxCollider2D boxCollider;
    float horizontalMoveInput;
    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask groundLayer;
    private bool isTouchingGround;
    public int extraJumps;
    private int jumpCounter;

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
    }

    void Jump()
    {
        body.velocity = new Vector2(body.velocity.x, jumpPower);
    }
}
