using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    [SerializeField] Slider musicSlider;
    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey("musicVolume"))
        {
            PlayerPrefs.SetFloat("musicVolume", .5f);
            loadPreference();
        }
        else
        {
            loadPreference();
        }
    }

    public void ChangeVolume()
    {
        AudioListener.volume = musicSlider.value;
        savePreference();
    }

    private void loadPreference()
    {
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
    }

    private void savePreference()
    {
        PlayerPrefs.SetFloat("musicVolume", musicSlider.value);
    }
}
