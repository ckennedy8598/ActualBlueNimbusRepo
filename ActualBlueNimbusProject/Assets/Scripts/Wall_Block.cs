using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall_Block : MonoBehaviour
{
    [SerializeField] public GameObject BlockingWall;
    private bool wallActive = false;
    // Start is called before the first frame update
    void Start()
    {
        BlockingWall.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && wallActive == false)
        {
            Debug.Log("Player Passed Wall Point");
            BlockingWall.SetActive(true);
            wallActive = true;
        }
        else
        {
            return;
        }
    }
}
