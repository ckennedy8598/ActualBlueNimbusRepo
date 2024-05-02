using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield_Enemy_Script : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Animator Reference")]
    public Animator anim;

    [Header("Rigid Body Reference")]
    private Rigidbody2D rb;

    [Header("Enemy Contact Damage")]
    [SerializeField] public int damage = 2;

    [Header("Player Health Variables")]
    public PlayerCombat playerHealth;
    public int maxHealth = 100;
    public float currentHealth;

    Looking_At_Player look;

    public int soulValue;
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
    public void Update()
    {
        look.LookAtPlayer();
    }

    public void EnemyTakeDamage()
    {

        Die();
        StartCoroutine(DestroyBody());

    }

     //Enemy to player collision damage
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Player Collision Damage");
            playerHealth.TakeDamage(damage);
        }
        if (collision.gameObject.tag == "FireBall")
        {
            Die();
            StartCoroutine(DestroyBody());
        }
    }

    public void Die()
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
        yield return new WaitForSeconds(1.7f);
        Destroy(gameObject);
        Collectibles_Soul_Counter.instance.IncreaseSouls(soulValue);
    }
}
