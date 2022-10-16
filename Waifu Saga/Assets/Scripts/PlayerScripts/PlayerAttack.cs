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


    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Attack"));
        {
            attack();
        }
    }

    void attack()
    {
        // Attack animation
        // Detect enemy
        // Damage enemy
    }
}
