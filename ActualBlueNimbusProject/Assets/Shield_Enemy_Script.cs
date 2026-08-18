/*
 * ****************************************************************************** *
 * Shield Enemy Script                                                            *
 *                                                                                *
 * Handles shield enemy health, player contact damage, death, and looking toward  *
 * the player.                                                                    *
 * ****************************************************************************** *
*/

using System.Collections;
using UnityEngine;

public class Shield_Enemy_Script : MonoBehaviour
{
    [Header("Animator Reference")]
    [SerializeField] private Animator anim;

    [Header("Enemy Contact Damage")]
    [SerializeField] private int damage = 2;

    [Header("Health")]
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private float currentHealth;

    [Header("Soul Reward")]
    [SerializeField] private int soulValue;

    private Rigidbody2D rb;
    private Collider2D enemyCollider;
    private PlayerCombat playerHealth;
    private Looking_At_Player look;

    private void Start()
    {
        currentHealth = maxHealth;

        // Get components attached to this enemy.
        rb = GetComponent<Rigidbody2D>();
        enemyCollider = GetComponent<Collider2D>();
        look = GetComponent<Looking_At_Player>();

        // Find the player's PlayerCombat component.
        playerHealth = FindObjectOfType<PlayerCombat>();

        if (rb == null)
        {
            Debug.LogError("Shield Enemy is missing a Rigidbody2D.", this);
        }

        if (enemyCollider == null)
        {
            Debug.LogError("Shield Enemy is missing a Collider2D.", this);
        }

        if (look == null)
        {
            Debug.LogError(
                "Shield Enemy is missing the Looking_At_Player script.",
                this
            );
        }

        if (playerHealth == null)
        {
            Debug.LogError(
                "PlayerCombat component not found in the scene.",
                this
            );
        }

        // Try to automatically find the Animator if one was not assigned.
        if (anim == null)
        {
            anim = GetComponent<Animator>();
        }

        if (anim == null)
        {
            Debug.LogError("Shield Enemy is missing an Animator.", this);
        }
    }

    private void Update()
    {
        if (look != null)
        {
            look.LookAtPlayer();
        }
    }

    public void EnemyTakeDamage()
    {
        Die();
        StartCoroutine(DestroyBody());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player Collision Damage");

            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }
        }

        if (collision.gameObject.CompareTag("FireBall"))
        {
            Die();
            StartCoroutine(DestroyBody());
        }
    }

    private void Die()
    {
        if (anim != null)
        {
            anim.SetTrigger("isDead");
        }

        if (rb != null)
        {
            rb.bodyType = RigidbodyType2D.Kinematic;

            // Freeze both X and Y movement.
            rb.constraints = RigidbodyConstraints2D.FreezePosition;
        }

        if (enemyCollider != null)
        {
            enemyCollider.enabled = false;
        }

        // Disable this script so the enemy stops running Update().
        enabled = false;
    }

    private IEnumerator DestroyBody()
    {
        yield return new WaitForSeconds(1.7f);

        if (Collectibles_Soul_Counter.instance != null)
        {
            Collectibles_Soul_Counter.instance.IncreaseSouls(soulValue);
        }
        else
        {
            Debug.LogError(
                "Collectibles_Soul_Counter instance could not be found.",
                this
            );
        }

        Destroy(gameObject);
    }
}