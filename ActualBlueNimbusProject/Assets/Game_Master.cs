using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Game_Master : MonoBehaviour
{
    // Start is called before the first frame update

    private static Game_Master instance;
    public Vector2 lastCheckpointPos;
    public TMP_Text soulText;

    public int currentSouls = 0;
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
       // soulText.text = "Souls: " + currentSouls.ToString();
    }
    public void IncreaseSouls(int x)
    {
        currentSouls += x;
        soulText.text = "Souls: " + currentSouls.ToString();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
