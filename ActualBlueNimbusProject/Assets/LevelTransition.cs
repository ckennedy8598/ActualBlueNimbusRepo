using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTransition : MonoBehaviour
{

    public Transform player;
    public Game_Master resetCheckPoint;
    
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            resetCheckPoint = GameObject.FindGameObjectWithTag("GM").GetComponent<Game_Master>();
            resetCheckPoint.lastCheckpointPos = new Vector2(0, 0);

        }
    }

    public void SetPosition()
    {
        
        player.transform.position = new Vector2(0, 0);
    }
    private void Awake()
    {
        
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
