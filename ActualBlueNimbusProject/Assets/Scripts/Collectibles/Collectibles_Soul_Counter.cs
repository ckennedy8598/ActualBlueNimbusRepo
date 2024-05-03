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
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(gameObject);
        }


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
        Soulnoise.Play();
        Debug.Log("SOUL SOUND PLAYED");
        // if anything goes wrong with the souls look here bc I (CK) added lines lmao
    }
    
}
