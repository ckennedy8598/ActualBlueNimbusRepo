/*
 * ****************************************************************************** *
 * Created by Bobby Lapadula                                                      *
 * Last Modified by Bobby Lapadula                                                *
 * Date and Time: 3/14/2024 15:15                                                 *
 *                                                                                *
 * This is the Enemy script. It is primarily a test and prefab script for future  *
 * enemy stats. These stats include health and damage. Functions in this script   *
 * can be called from other scripts using getComponent. This script references    *
 * the PlayerCombat script.                                                       *
 * ****************************************************************************** *
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Contact Damage")]
    [SerializeField] public int damage = 2;

    public PlayerCombat playerHealth;
    public int maxHealth = 100;
    public int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;

        // Instantiate PlayerCombat script reference
        playerHealth = FindObjectOfType<PlayerCombat>();
        if (playerHealth == null)
        {
            Debug.LogError("PlayerCombat component not found in the scene.");
        }
    }

    public void EnemyTakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // Enemy to player collision damage
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerHealth.TakeDamage(damage);
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
