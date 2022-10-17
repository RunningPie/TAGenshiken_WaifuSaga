using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    private Rigidbody2D body;

    [Header("Enemy Stats")]
    public float enemyHealth;
    [SerializeField] float currentHealth;
    public float enemySpeed;
    public Transform target;
    [SerializeField] Vector2 moveDirection;

    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = enemyHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if(target){
            moveDirection = (target.position - transform.position);
            if (moveDirection.x < -1.6f)
            {
                transform.Translate(Vector2.left * enemySpeed);
                transform.localScale = new Vector2(-5, 5);
            }
            else if (moveDirection.x > 1.6f)
            {
                transform.Translate(Vector2.right * enemySpeed);
                transform.localScale = new Vector2(5, 5);
            }
            else
            {
                body.velocity = Vector2.zero;
            }
        }
    }

    void FixedUpdate()
    {

    }
}
