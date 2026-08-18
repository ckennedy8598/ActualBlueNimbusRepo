using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Looking_At_Player : MonoBehaviour
{
    private Transform player;

    public bool isFlipped = false;

    private void Start()
    {
        FindPlayer();
    }

    private void FindPlayer()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

        if (playerObject != null)
        {
            player = playerObject.transform;
        }
    }

    public void LookAtPlayer()
    {
        // If the player was destroyed or has not been found yet,
        // attempt to find the current Player object.
        if (player == null)
        {
            FindPlayer();

            // There may currently be no player, such as during death.
            if (player == null)
            {
                return;
            }
        }

        Vector3 flipped = transform.localScale;
        flipped.z *= -1f;

        if (transform.position.x > player.position.x && isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = false;
        }
        else if (transform.position.x < player.position.x && !isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = true;
        }
    }
}