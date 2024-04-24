using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield_Knight_Script : MonoBehaviour
{
    // Start is called before the first frame update
    private Enemy Enemy;
    void Start()
    {
        
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "FireBall")
        {
            
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
