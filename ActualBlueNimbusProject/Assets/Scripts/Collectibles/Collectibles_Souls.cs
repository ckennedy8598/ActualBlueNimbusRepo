using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectibles_Souls : MonoBehaviour
{
    public int x;
    private Game_Master gm;

    private void OnTriggerEnter2D(Collider2D other)
    {
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<Game_Master>();

        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            gm.IncreaseSouls(x);
        }
    }
}
