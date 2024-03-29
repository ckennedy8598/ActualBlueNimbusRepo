/*
 * ****************************************************************************** *
 * Created by Bobby Lapadula                                                      *
 * Last Modified by Colin Murray                                                  *
 * Date and Time: 3/18/2024 XX:XX                                                 *
 *                                                                                *
 * This is the player combat script. It encapsulates all methods and variables    *
 * required for the player to engage in combat. This script also has functions to *
 * kill the player and, once dead, displays a lose screen with retry and main     *
 * menu buttons.                                                                  *
 * ****************************************************************************** *
*/
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCombat : MonoBehaviour
{
    [Header("Player Enemy Collider Reference")]
    private Collider2D coll;

    [Header("User Interface Variables")]
    [SerializeField] public TMP_Text loseText;
    [SerializeField] public Button retry;
    [SerializeField] public Button mainMenu;

    [Header("Damage Variables")]
    public LayerMask enemyLayers;
    public Transform attackPoint;
    private float attackRange = .5f;
    private int attackDamage = 50;

    [Header("Health Variables")]
    public int playerHealth = 0;
    public bool canBeHit = true;
    private int maxHealth = 5;
    public Slider slider;
    
    private void Start()
    {
        coll = GetComponent<Collider2D>();
        playerHealth = maxHealth; 

        loseText.enabled = false;
        retry.gameObject.SetActive(false);
        mainMenu.gameObject.SetActive(false);
        
        // Moved as when unassigned they were throwing an error
        // and returning out of Start() before other code was
        // being set.
        slider.maxValue = maxHealth;
        slider.value = playerHealth;
    }

    void Update()
    {
        // If player object does not exist, return
        if (gameObject == null)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Attack();
        }

        /* Work on making invulnerability able to pass through enemies
        if (Input.GetKeyDown(KeyCode.F))
        {
            coll.enabled = !coll.enabled;
        }
        */
    }

    // Gets array of enemies hit and returns each dealing player attack damage to them
    void Attack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach(Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().EnemyTakeDamage(attackDamage);
        }
    }

    public void TakeDamage(int damage)
    {
        playerHealth -= damage; 
        slider.value = playerHealth;

        if (playerHealth <= 0)
        {
            Die();
            LoseScreen();
        }
    }

    // Draw circle for attack position
    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    /*
    private IEnumerator invulnerablePeriod()
    {
        canBeHit = false;
    }
    */

    // If player object exists, destroy
    public void Die()
    {
        if (gameObject != null)
        {
            Destroy(gameObject);
        }
    }

    // Display losing text and menuing buttons
    public void LoseScreen()
    {
        loseText.enabled = true;
        retry.gameObject.SetActive(true);
        mainMenu.gameObject.SetActive(true);
    }
}
