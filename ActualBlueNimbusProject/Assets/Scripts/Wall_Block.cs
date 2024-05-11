using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall_Block : MonoBehaviour
{
    [SerializeField] public GameObject BlockingWall;
    [SerializeField] public GameObject BlockingWall3;
    [SerializeField] public GameObject BlockingWall4;
    private bool wallActive = false;
    // Start is called before the first frame update
    void Start()
    {
        BlockingWall.SetActive(false);
        BlockingWall4.SetActive(false);
        BlockingWall3.SetActive(true);
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
            BlockingWall4.SetActive(true);
            BlockingWall3.SetActive(false);
            wallActive = true;
        }
        else
        {
            return;
        }
    }
}
