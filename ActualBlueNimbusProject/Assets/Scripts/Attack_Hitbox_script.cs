using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_Hitbox_script : MonoBehaviour
{
    public PlayerCombat playerHealth;
    [SerializeField] public int damage = 2;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Player Collision Damage");
            playerHealth.TakeDamage(damage);
        }
    }
}
