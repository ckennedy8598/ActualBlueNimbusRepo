using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public Player playerScript;
    private GameObject player;
    private Rigidbody2D rb;
    [SerializeField] private float force;
    private float timer;
    [SerializeField] private float despawnTimer = 10;
    private bool isRight = true;

    [SerializeField] public AudioClip FireballSound;

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

    // Update is called once per frame
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

        timer += Time.deltaTime;

        if (timer > despawnTimer)
        {
            Destroy(gameObject);
        }
    }

    // This gets called whenever the Arrow collides with something

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
