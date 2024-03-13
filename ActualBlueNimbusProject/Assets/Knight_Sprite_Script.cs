using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
// All this does is flip the knight's sprite when they are pathfinding to the player and the player gets on their other side
//
// This script is going to be replaced if we end up getting turn around animations
//
// -Chris

public class Knight_Sprite_Script : MonoBehaviour
{
    public AIPath aiPath;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(aiPath.desiredVelocity.x >= 0.01f)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else if (aiPath.desiredVelocity.x <= -0.01f)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }
}
