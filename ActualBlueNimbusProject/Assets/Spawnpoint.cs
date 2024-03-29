using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawnpoint : MonoBehaviour
{
    // Start is called before the first frame update
    private Game_Master gm;
    public void SpawnStart()
    {
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<Game_Master>();
        transform.position = gm.lastCheckpointPos;
        Debug.Log("Setting Initial spawnpoint");
    }
    
}
