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

    private int damage = 2;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerScript = FindObjectOfType<Player>();
        var variableNassme = player.transform.localScale;

        if (playerScript.GetFacingRight())
        {
            transform.Translate(transform.right);
            isRight = true;
        }
        else
        {
            transform.Translate(-transform.right);
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
        if (other.gameObject.CompareTag("Enemy"))
        {
            if (other.GetComponent<Enemy>() != null)
            {
                other.GetComponent<Enemy>().EnemyTakeDamage(damage);
            }

            if (other.GetComponent<enemScriptKnight>() != null)
            {
                other.GetComponent<enemScriptKnight>().KnightEnemyTakeDamage(damage);
            }
            Destroy(gameObject);
        }
        else
        {
            //Destroy(gameObject);
        }
    }
}
