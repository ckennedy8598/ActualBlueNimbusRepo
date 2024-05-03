using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Game_Master : MonoBehaviour
{
    // Start is called before the first frame update

    private static Game_Master instance;
    public Vector2 lastCheckpointPos;

    public static int totalSouls;
    public Collectibles_Soul_Counter CS;
    private void Awake()
    {
        
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        } else
        {
            Destroy(gameObject);
        }

        
    }
    void Start()
    {
        CS = GetComponent<Collectibles_Soul_Counter>();
    }

    
    // Update is called once per frame
    void Update()
    {
        
        if (CS.currentSouls >= totalSouls)
        {
            totalSouls = CS.currentSouls;
        }
        else if (totalSouls > CS.currentSouls)
        {
            CS.currentSouls = totalSouls;
        }
    }

}
