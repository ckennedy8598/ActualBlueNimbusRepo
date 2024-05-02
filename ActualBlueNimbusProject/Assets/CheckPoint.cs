using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public PlayerCombat playerHealth;
    private Game_Master gm;
    
    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<Game_Master>();
        playerHealth = GameObject.FindAnyObjectByType<PlayerCombat>();
    }
   

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            gm.lastCheckpointPos = transform.position;
            playerHealth.playerHealth = playerHealth.maxHealth;
            Debug.Log("Checkpoint");
        }
    }

    

    // Update is called once per frame
    void Update()
    {
        
    }
}
