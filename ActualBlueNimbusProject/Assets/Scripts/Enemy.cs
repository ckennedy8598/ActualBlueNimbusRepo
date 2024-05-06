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
    [Header("Animator Reference")]
    public Animator anim;

    [Header("Rigid Body Reference")]
    private Rigidbody2D rb;

    [Header("Enemy Contact Damage")]
    [SerializeField] public int damage = 2;
    [SerializeField] private bool canDoContactDamage = true;

    [Header("Player Health Variables")]
    public PlayerCombat playerHealth;
    public int maxHealth = 100;
    public float currentHealth;

    public int soulValue;
    [SerializeField] private float deathTimer = 2.5f;
    void Start()
    {
        currentHealth = maxHealth;

        rb = GetComponent<Rigidbody2D>();

        // Instantiate PlayerCombat script reference
        playerHealth = FindObjectOfType<PlayerCombat>();
        if (playerHealth == null)
        {
            Debug.LogError("PlayerCombat component not found in the scene.");
        }
    }

    public void EnemyTakeDamage(float damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
            StartCoroutine(DestroyBody());
        }
    }

    // Enemy to player collision damage
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && canDoContactDamage)
        {
            Debug.Log("Player Collision Damage");
            playerHealth.TakeDamage(damage);
        }
    }

    private void Die()
    {
        anim.SetTrigger("isDead");

        gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        // Freeze X and Y position of object rigidbody
        rb.constraints = RigidbodyConstraints2D.FreezePositionX;
        rb.constraints = RigidbodyConstraints2D.FreezePositionY;
        gameObject.GetComponent<Collider2D>().enabled = false;
        // Disables Enemy Script
        this.enabled = false;
    }

    private IEnumerator DestroyBody()
    {
        yield return new WaitForSeconds(deathTimer);
        Destroy(gameObject);
        Collectibles_Soul_Counter.instance.IncreaseSouls(soulValue);
    }
}
