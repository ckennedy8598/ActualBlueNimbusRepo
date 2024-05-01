using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Axe_Hitbox : MonoBehaviour
{
    // Start is called before the first frame update
    
    public PlayerCombat playerHealth;
    [SerializeField] public int damage = 2;

    void Start()
    {
        playerHealth = FindObjectOfType<PlayerCombat>();
        if (playerHealth == null)
        {
            Debug.LogError("PlayerCombat component not found in the scene.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(UnityEngine.Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerHealth.TakeDamage(damage);
            Debug.Log("Player Taking Damage");
        }
    }
}
