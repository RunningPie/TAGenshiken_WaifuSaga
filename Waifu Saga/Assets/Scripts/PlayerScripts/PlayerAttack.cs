using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour

{
    [Header ("Melee attack")]
    public Transform hurtbox;
    public float hurtboxRange;
    public LayerMask enemy;
    public float damage;
    public float attackSpeed;
    [SerializeField] private bool canAttack;

    void Start()
    {
        canAttack = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Attack") && canAttack)
        {
            Collider2D[] enemies = Physics2D.OverlapCircleAll(hurtbox.position, hurtboxRange, enemy);
            foreach (Collider2D enemy in enemies)
            {
                Debug.Log(enemy.name + " hit");
            }
            canAttack = false;
            StartCoroutine(attackCooldown());
        }
        IEnumerator attackCooldown()
        {
            yield return new WaitForSeconds(attackSpeed);
            canAttack = true;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(hurtbox.position, hurtboxRange);
    }
}
