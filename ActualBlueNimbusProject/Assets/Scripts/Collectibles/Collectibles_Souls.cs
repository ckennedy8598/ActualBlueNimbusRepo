using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectibles_Souls : MonoBehaviour
{
    public int x;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            Collectibles_Soul_Counter.instance.IncreaseSouls(x);
        }
    }
}
