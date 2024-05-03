using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Collectibles_Soul_Counter : MonoBehaviour
{
    public static Collectibles_Soul_Counter instance;
    [SerializeField] private AudioSource Soulnoise;

    public TMP_Text soulText;
    public int currentSouls = 0;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        soulText.text = "Souls: " + GetInt("Souls");
        if (GetInt("Souls") > 0)
        {
            currentSouls = GetInt("Souls");
            return;
        }
        else
        {
            PlayerPrefs.SetInt("Souls", 0);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void IncreaseSouls(int x)
    {
        currentSouls += x;
        PlayerPrefs.SetInt("Souls", currentSouls);
        soulText.text = "Souls: " + GetInt("Souls");
        Soulnoise.Play();
        Debug.Log("SOUL SOUND PLAYED");
        // if anything goes wrong with the souls look here bc I (CK) added lines lmao
    }

    public void SetInt(string KeyName, int Value)
    {
        PlayerPrefs.SetInt("souls", Value);
    }

    public int GetInt(string KeyName)
    {
        return PlayerPrefs.GetInt(KeyName);
    }
}
