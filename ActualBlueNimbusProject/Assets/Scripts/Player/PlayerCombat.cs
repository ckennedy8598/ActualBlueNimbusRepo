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
using System.Runtime.InteropServices.WindowsRuntime;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCombat : MonoBehaviour
{
    [Header("Player Enemy Collider Reference")]
    private Collider2D coll;

    [Header("Animation Handlers")]
    public Animator animator;

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

    [Header("Attack Variables")]
    public float attackRate = 2f;
    float nextAttackTime = 0f;
    private bool heavyInput = false;

    [Header("Heavy Attack Variables")]
    public GameObject Fireball;
    public Transform FireballPos;
    public bool isCharging = false;
    public float maxChargeTime = 1f;
    private float minDamage = 25f;
    private float maxDamage = 100f;
    private float currentChargeTime = 0f;



    [Header("Sound Effects")]
    [SerializeField] private AudioSource attackSFX;
    [SerializeField] private AudioSource deathSFX;
    
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

        if (Time.time >= nextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0) && isCharging == false)
            {
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }

        // Heavy Attack Input
        if (isCharging == false)
        {
            if (Input.GetMouseButton(1))
            {
                currentChargeTime = 0f;
                animator.SetTrigger("isCharging");
                animator.ResetTrigger("doneAttacking");
                //Debug.Log("isCharging: true");
                isCharging = true;
            }
        }

        // Start Charge Time
        if (isCharging)
        {
            currentChargeTime += Time.deltaTime;
            currentChargeTime = Mathf.Clamp(currentChargeTime, 0f, maxChargeTime);
            //Debug.Log("Current Charge Time: " + currentChargeTime);
        }

        // Heavy Attack Release + Calculate Damage Ratio
        if (Input.GetMouseButtonUp(1) && heavyInput == false)
        {
            if (isCharging)
            {
                Debug.Log("Heavy Attacking");
                animator.SetTrigger("isHeavyAttacking");

                heavyInput = true;
                float chargeRatio = currentChargeTime / maxChargeTime;
                float damage = Mathf.Lerp(minDamage, maxDamage, chargeRatio);
                if (currentChargeTime == maxChargeTime)
                {
                    Fireball_Attack();
                    Debug.Log("Fireball!!!!!!!");
                }
                StartCoroutine(heavyAttackCD(damage));
            }
        }
    }

    // Blinking Sprite Upon Taking Damage for Invul Duration

    // Gets array of enemies hit and returns each dealing player attack damage to them
    void Attack()
    {
        // Play Attack Animation
        animator.SetTrigger("Light_Attack");

        // Play Attack Sound
        attackSFX.Play();

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach(Collider2D enemy in hitEnemies)
        {
            if (enemy.GetComponent<Enemy>() != null)
            {
                enemy.GetComponent<Enemy>().EnemyTakeDamage(attackDamage);
            }

            if (enemy.GetComponent<enemScriptKnight>() != null)
            {
                enemy.GetComponent<enemScriptKnight>().KnightEnemyTakeDamage(attackDamage);
            }
        }
    }

    void heavyAttack(float damage)
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            if (enemy.GetComponent<Enemy>() != null)
            {
                enemy.GetComponent<Enemy>().EnemyTakeDamage(damage);
            }

            if (enemy.GetComponent<enemScriptKnight>() != null)
            {
                enemy.GetComponent<enemScriptKnight>().KnightEnemyTakeDamage(damage);
            }
        }
    }

    public void TakeDamage(int damage)
    {
        if (canBeHit)
        {
            playerHealth -= damage;
            slider.value = playerHealth;
/*            deathSFX.Play();
            Debug.Log("Played");*/
            StartCoroutine(Invul());
            //Debug.Log("State of CanBeHit: " + canBeHit);
        }

        if (playerHealth <= 0)
        {
            Die();
            LoseScreen();
        }
    }

    private IEnumerator heavyAttackCD(float damage)
    {
        //Debug.Log("Heavy Attack: Start");
        yield return new WaitForSeconds(.85f);
        heavyAttack(damage);
        currentChargeTime = 0f;
        isCharging = false;
        heavyInput = false;
        animator.SetTrigger("doneAttacking");
        //Debug.Log("Heavy Attack: End");
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

    public void Fireball_Attack()
    {
        Instantiate(Fireball, FireballPos.position, Fireball.transform.rotation);
    }

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

    public IEnumerator Invul()
    {
        CanBeHit();
        yield return new WaitForSeconds(2);
        CanBeHit();
    }

    public void CanBeHit()
    {
        canBeHit = !canBeHit;
    }

    public bool GetIsCharging()
    {
        return isCharging;
    }
}
