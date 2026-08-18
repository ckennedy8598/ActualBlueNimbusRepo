using UnityEngine;

public class EnemyArrowScript : MonoBehaviour
{
    /// <summary>
    /// Shooting and AI script for the Archer's arrow projectile.
    ///
    /// Created by Christopher Bunnell
    /// Last Modified by Bobby Lapadula 3/15/2024 15:15
    /// Updated 8/18/2026 to add null-reference protection.
    /// </summary>

    private GameObject player;
    private Rigidbody2D rb;

    [Header("Arrow Movement")]
    [SerializeField] private float force;

    [Header("Arrow Lifetime")]
    [SerializeField] private float despawnTimer = 10f;

    [Header("Enemy Contact Damage")]
    [SerializeField] private int damage = 1;

    private float timer;

    void Start()
    {
        // Get the arrow's Rigidbody2D.
        rb = GetComponent<Rigidbody2D>();

        if (rb == null)
        {
            Debug.LogError(
                "EnemyArrowScript: Arrow does not have a Rigidbody2D.",
                gameObject);

            Destroy(gameObject);
            return;
        }

        // Find the active player.
        player = GameObject.FindGameObjectWithTag("Player");

        if (player == null)
        {
            Debug.LogWarning(
                "EnemyArrowScript: No active GameObject with the Player tag was found. " +
                "Destroying this arrow.",
                gameObject);

            Destroy(gameObject);
            return;
        }

        // Calculate the direction toward the player.
        Vector3 direction =
            player.transform.position - transform.position;

        // Launch the arrow.
        rb.velocity =
            new Vector2(direction.x, direction.y).normalized * force;

        // Rotate the arrow toward the direction it is traveling.
        float rot =
            Mathf.Atan2(-direction.y, -direction.x) *
            Mathf.Rad2Deg;

        transform.rotation =
            Quaternion.Euler(0f, 0f, rot + 90f);
    }

    void Update()
    {
        timer += Time.deltaTime;

        // Destroy the arrow after its maximum lifetime.
        if (timer >= despawnTimer)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // If the arrow hits the player, attempt to damage them.
        if (other.CompareTag("Player"))
        {
            PlayerCombat playerHealth =
                other.GetComponent<PlayerCombat>();

            // The player's collider may be on a child object,
            // so check the parent as well.
            if (playerHealth == null)
            {
                playerHealth =
                    other.GetComponentInParent<PlayerCombat>();
            }

            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }
            else
            {
                Debug.LogWarning(
                    "EnemyArrowScript: Hit an object tagged Player, " +
                    "but could not find PlayerCombat.",
                    other.gameObject);
            }

            Destroy(gameObject);
            return;
        }

        // Destroy the arrow when it hits anything else.
        Destroy(gameObject);
    }
}