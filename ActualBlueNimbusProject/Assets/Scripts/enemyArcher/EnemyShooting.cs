using System.Collections;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    [Header("Animator Reference")]
    [SerializeField] private Animator anim;

    [Header("Arrow References")]
    [SerializeField] private GameObject Arrow;
    [SerializeField] private Transform ArrowPos;

    [Header("Shooting Settings")]
    [SerializeField] private float shootInterval = 2f;
    [SerializeField] private float sightRange = 4f;

    private GameObject player;
    private Enemy enemy;
    private float timer;

    private void Start()
    {
        enemy = GetComponent<Enemy>();

        FindPlayer();
    }

    private void Update()
    {
        // If the enemy component is missing, do nothing.
        if (enemy == null)
        {
            return;
        }

        // If this enemy is dead, do nothing.
        if (enemy.currentHealth <= 0)
        {
            return;
        }

        // The original Player may have been destroyed during death/respawning.
        // Try to find the current Player if our reference is gone.
        if (player == null)
        {
            FindPlayer();

            // If there still isn't a Player in the scene, wait until next frame.
            if (player == null)
            {
                return;
            }
        }

        FacePlayer();

        float distance = Vector2.Distance(
            transform.position,
            player.transform.position
        );

        if (distance < sightRange)
        {
            timer += Time.deltaTime;

            if (timer >= shootInterval)
            {
                timer = 0f;
                StartCoroutine(ShootAnim());
            }
        }
        else
        {
            // Optional:
            // Prevents accumulated time from immediately firing
            // when the player re-enters the sight range.
            timer = 0f;
        }
    }

    private void FindPlayer()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void FacePlayer()
    {
        Vector3 scale = transform.localScale;

        if (player.transform.position.x > transform.position.x)
        {
            scale.x = -Mathf.Abs(scale.x);
        }
        else
        {
            scale.x = Mathf.Abs(scale.x);
        }

        transform.localScale = scale;
    }

    private void Shoot()
    {
        // Don't try to spawn an arrow if the spawn point
        // or arrow prefab has disappeared.
        if (Arrow == null || ArrowPos == null)
        {
            return;
        }

        Instantiate(Arrow, ArrowPos.position, Quaternion.identity);
    }

    private IEnumerator ShootAnim()
    {
        if (anim != null)
        {
            anim.SetTrigger("Attack");
        }

        yield return new WaitForSeconds(1f);

        // The enemy could have died during the one-second delay.
        if (enemy == null || enemy.currentHealth <= 0)
        {
            yield break;
        }

        Shoot();
    }
}