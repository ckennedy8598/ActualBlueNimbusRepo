/*
 * ****************************************************************************** *
 * Created by Bobby Lapadula                                                      *
 * Last Modified by Bobby Lapadula                                                *
 * Date and Time: 3/15/2024 14:16                                                 *
 *                                                                                *
 * This is the main menu script. It contains the methods used for user interface  *
 * button interactions such as retry level and quit to main menu. This is all it  *
 * does.                                                                          *
 * ****************************************************************************** *
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] Slider musicSlider;
    public void PlayGame()
    {
        SceneManager.LoadScene(1);
        setVolume();
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Main_Menu()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private void setVolume()
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

    private void loadPreference()
    {
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
    }
}
