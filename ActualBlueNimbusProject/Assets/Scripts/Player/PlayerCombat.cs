/*
 * ****************************************************************************** *
 * Last Modified by Bobby Lapadula                                                *
 * Date and Time: 2/25/2024 XX:XX                                                 *
 *                                                                                *
 * This is the player combat script. It encapsulates all methods and variables    *
 * required for the player to engage in combat.                                   *
 * ****************************************************************************** *
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public LayerMask enemyLayers;
    public Transform attackPoint;
    public float attackRange = .5f;
    public int attackDamage = 50;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Attack();
        }
    }

    void Attack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach(Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
