using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Collectibles_Soul_Counter : MonoBehaviour
{
    public static Collectibles_Soul_Counter instance;

    public TMP_Text soulText;
    private Game_Master gm;

    private int soulDisplay;
    private void Awake()
    {
        instance = this;
        soulDisplay = gm.currentSouls;
    }
    // Start is called before the first frame update
    void Start()
    {
        soulText.text = "Souls: " + soulDisplay.ToString();
    }

    // Update is called once per frame
    void Update()
    {

    }

   // public void IncreaseSouls(int x)
   // {
   //     currentSouls += x;
   //     soulText.text = "Souls: " + currentSouls.ToString();
   // }
}
