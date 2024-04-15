using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPos : MonoBehaviour
{
    // Start is called before the first frame update
    private Game_Master gm;
    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<Game_Master>();
        transform.position = gm.lastCheckpointPos;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
