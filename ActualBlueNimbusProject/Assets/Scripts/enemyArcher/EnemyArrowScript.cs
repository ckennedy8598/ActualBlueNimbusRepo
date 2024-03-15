using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class EnemyArrowScript : MonoBehaviour
{

    /// 
    /// This is the shooting and Ai script for the Bullet Object
    /// For the most part, This *should* be good enough for a while
    /// When the damage values are added, put the logic in the "On Trigger Enter 2D"
    ///         - Christopher Bunnell
    /// 
    /// Created by Christopher Bunnell
    /// Last Modified by Bobby Lapadula 3/15/2024 15:15
    private GameObject player;
    private Rigidbody2D rb;
    [SerializeField] private float force;
    private float timer;
    [SerializeField] private float despawnTimer = 10;

    // Lines 21 - 26 Added by Bobby;
    [Header("Enemy Contact Damage")]
    [SerializeField] public int damage = 1;

    // Calling TakeDamage function from PlayerCombat script;
    public PlayerCombat playerHealth;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");

        Vector3 direction = player.transform.position - transform.position;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * force;

        float rot = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot + 90);

        // Initiates PlayerCombat script and checks for null.
        playerHealth = FindObjectOfType<PlayerCombat>();
        if (playerHealth == null)
        {
            Debug.LogError("PlayerCombat component not found in the scene.");
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            return;
        }

        timer += Time.deltaTime;

        if (timer > despawnTimer)
        {
            Destroy(gameObject);
        }
    }

    // This gets called whenever the Arrow collides with something

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerHealth.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
