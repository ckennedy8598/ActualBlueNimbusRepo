using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Collectibles_Soul_Counter : MonoBehaviour
{
    public static Collectibles_Soul_Counter instance;

    public TMP_Text soulText;
    public int currentSouls = 0;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        soulText.text = "Souls: " + currentSouls.ToString();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void IncreaseSouls(int x)
    {
        currentSouls += x;
        soulText.text = "Souls: " + currentSouls.ToString();
    }
}
