/*
 * ****************************************************************************** *
 * Created by Carmen idkyourlastname:(                                            *
 * Last Modified by Bobby Lapadula                                                *
 * Date and Time: 3/15/2024 14:16                                                 *
 *                                                                                *
 * This is the instant death spike hazard script. When the player collides with a *
 * spike, this script kills them and displays a you lose menu with retry and      *
 * main menu buttons from the PlayerCombat script.                                *
 * ****************************************************************************** *
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetOnHazard : MonoBehaviour
{
    public PlayerCombat playerHealth;

    void Start()
    {
        // Instantiate PlayerCombat script reference
        playerHealth = FindObjectOfType<PlayerCombat>();
        if (playerHealth == null)
        {
            Debug.LogError("PlayerCombat component not found in the scene.");
        }
    }

    // References from PlayerCombat script
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerHealth.Die();
            playerHealth.LoseScreen();
        }
    }
}
