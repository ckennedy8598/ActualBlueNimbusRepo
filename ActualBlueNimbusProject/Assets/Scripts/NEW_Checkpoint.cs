using UnityEngine;

public class NEW_Checkpoint : MonoBehaviour
{
    [Header("Checkpoint Settings")]
    [SerializeField] private bool healPlayer = true;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
        {
            return;
        }

        if (NEW_GameManager.Instance == null)
        {
            Debug.LogError(
                "NEW_Checkpoint: No NEW_GameManager exists."
            );

            return;
        }

        // Save the Player's actual current position.
        Vector2 playerPosition =
            (Vector2)other.transform.position;

        NEW_GameManager.Instance.SetCheckpoint(
            playerPosition
        );

        // Preserve the behavior of your old CheckPoint script.
        if (healPlayer)
        {
            PlayerCombat playerCombat =
                other.GetComponent<PlayerCombat>();

            if (playerCombat != null)
            {
                playerCombat.playerHealth =
                    playerCombat.maxHealth;

                if (playerCombat.slider != null)
                {
                    playerCombat.slider.value =
                        playerCombat.playerHealth;
                }
            }
        }

        Debug.Log(
            "NEW_Checkpoint: Checkpoint activated at " +
            playerPosition
        );
    }
}