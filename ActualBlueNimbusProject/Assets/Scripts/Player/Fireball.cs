/*
 * ****************************************************************************** *
 * Created by Bobby Lapadula                                                      *
 * Last Modified by Bobby Lapadula                                                *
 * Date and Time: 4/128/2024 24:00                                                *
 *                                                                                *
 * This is the fireball script. It handles everything about the fireball. It is   *
 * called when the fireball object is instantiated by the player's max charge     *
 * attack. It references the Player script.                                       *
 * ****************************************************************************** *
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    [SerializeField] public AudioClip FireballSound;
    [SerializeField] private float force;
    [SerializeField] private float despawnTimer = 10;

    public Player playerScript;
    private GameObject player;
    private Rigidbody2D rb;
    private float timer;
    private bool isRight = true;
    private int damage = 50;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerScript = FindObjectOfType<Player>();
        Vector3 fireballScaler = transform.localScale;


        if (playerScript.GetFacingRight())
        {
            transform.Translate(transform.right);
            isRight = true;
        }
        else
        {
            transform.Translate(-transform.right);
            fireballScaler.x *= -1;
            transform.localScale = fireballScaler;
            isRight = false;
        }
    }

    void Update()
    {
        if (isRight)
        {
            transform.Translate((transform.right * force * Time.deltaTime));
        }
        else
        {
            transform.Translate((-transform.right * force * Time.deltaTime));
        }

        // Despawn timer for projectile
        timer += Time.deltaTime;

        if (timer > despawnTimer)
        {
            Destroy(gameObject);
        }
    }

    // This gets called whenever the Fireball collides with something
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Hit Enemy!");
            if (other.GetComponent<Enemy>() != null)
            {
                other.GetComponent<Enemy>().EnemyTakeDamage(damage);
                Debug.Log("Hit Archer!");
            }

            if (other.GetComponent<enemScriptKnight>() != null)
            {
                other.GetComponent<enemScriptKnight>().KnightEnemyTakeDamage(damage);
                Debug.Log("Hit Knight!");
            }
            if (other.GetComponent<Shield_Enemy_Script>() != null)
            {
                other.GetComponent<Shield_Enemy_Script>().EnemyTakeDamage();
                Debug.Log("Hit Shield Knight!");
            }
            AudioSource.PlayClipAtPoint(FireballSound, this.gameObject.transform.position);
            Destroy(gameObject);
        }
        else
        {
            AudioSource.PlayClipAtPoint(FireballSound, this.gameObject.transform.position);
            Destroy(gameObject);
        }
    }
}
