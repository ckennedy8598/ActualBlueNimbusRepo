using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingScript : MonoBehaviour
{
    public GameObject Swing;
    public Transform SwingPos;

    public float SwingRange = 3;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void EnemySwing()
    {
        Instantiate(Swing, SwingPos.position, Quaternion.identity);
    }
    void OnTriggerEnter2D(Collider other)
    {
        EnemySwing();
    }
}
