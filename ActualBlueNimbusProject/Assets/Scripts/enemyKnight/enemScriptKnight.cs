using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemScriptKnight : MonoBehaviour
{
    [Header("Animator Reference")]
    public Animator anim;

    [Header("Enemy Contact Damage")]
    [SerializeField] public int damage = 2;

    [Header("Player Health Variables")]
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

    public void KnightEnemyTakeDamage(int damage)
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
        anim.SetTrigger("isDead");

        gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
        //yield return new WaitForSeconds(2.5f);
        //Destroy(gameObject);
    }
}
