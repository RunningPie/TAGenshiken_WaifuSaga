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

    // Start is called before the first frame update

    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();

    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMoveInput = Input.GetAxis("Horizontal");
        
        if (horizontalMoveInput > 0.01f)
            transform.localScale = new Vector2(5, 5);
        else if (horizontalMoveInput < -0.01f)
            transform.localScale = new Vector2(-5, 5);
        
        body.velocity = new Vector2(horizontalMoveInput * speed, body.velocity.y);

        if(Input.GetKey(KeyCode.Space))
                Jump();
    }

    void Jump()
    {
        body.velocity = new Vector2(body.velocity.x, jumpPower);
    }
}
