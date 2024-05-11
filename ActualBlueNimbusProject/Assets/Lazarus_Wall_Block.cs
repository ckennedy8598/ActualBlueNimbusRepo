using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lazarus_Wall_Block : MonoBehaviour
{
    [SerializeField] public GameObject BlockingWall;
    [SerializeField] public GameObject BlockingWall2;
    private bool wallActive = false;
    public GameObject enemy_Boss_Lazarus;
    // Start is called before the first frame update
    void Start()
    {
        BlockingWall.SetActive(false);
        BlockingWall2.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (enemy_Boss_Lazarus == null)
        {
            BlockingWall.SetActive(false);
            BlockingWall2.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && wallActive == false)
        {
            Debug.Log("Player Passed Wall Point");
            BlockingWall2.SetActive(true);
            BlockingWall.SetActive(true);
            wallActive = true;
        }
        else
        {
            return;
        }
    }
}
