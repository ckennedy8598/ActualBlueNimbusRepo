using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemScriptKnight : MonoBehaviour
{
    [Header("Rigidbody2D Reference")]
    private Rigidbody2D rb;

    [Header("Animator Reference")]
    public Animator anim;

    [Header("Enemy Contact Damage")]
    [SerializeField] public int damage = 2;

    [Header("Player Health Variables")]
    public PlayerCombat playerHealth;
    public int maxHealth = 100;
    public int currentHealth;

    public int soulValue;
    public Game_Master gm;

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

    public void KnightEnemyTakeDamage(int damage)
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
        if (collision.gameObject.tag == "Player")
        {
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
        yield return new WaitForSeconds(2.5f);
        Destroy(gameObject);
        gm.IncreaseSouls(soulValue);
    }
}
