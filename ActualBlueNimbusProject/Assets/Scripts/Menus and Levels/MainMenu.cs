/*
 * ****************************************************************************** *
 * Created by Bobby Lapadula                                                      *
 * Last Modified by CK (8-18-26)                                                  *
 *                                                                                *
 * This is the main menu script. It contains the methods used for user interface  *
 * button interactions such as play, retry, credits, main menu, quit, and music   *
 * volume preference loading.                                                     *
 * ****************************************************************************** *
*/

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Slider musicSlider;

    [Header("Game References")]
    public Game_Master GameMaster;
    public PauseMenu PauseMenuScript;

    private void Awake()
    {
        // Try to find Game_Master if it has not been manually assigned.
        if (GameMaster == null)
        {
            GameMaster = FindObjectOfType<Game_Master>(true);
        }

        // Try to find PauseMenu if it has not been manually assigned.
        if (PauseMenuScript == null)
        {
            PauseMenuScript = FindObjectOfType<PauseMenu>(true);
        }

        // Safely initialize the PauseMenu.
        if (PauseMenuScript != null)
        {
            PauseMenuScript.setIsPaused();
        }
        else
        {
            Debug.LogWarning(
                "MainMenu: No PauseMenu component was found. " +
                "The main menu will continue without initializing PauseMenu."
            );
        }

        // Safely set the starting checkpoint position.
        if (GameMaster != null)
        {
            GameMaster.lastCheckpointPos = new Vector2(26.33f, -3.61f);
        }
        else
        {
            Debug.LogWarning(
                "MainMenu: No Game_Master component was found. " +
                "The starting checkpoint position could not be set."
            );
        }

        // Load the saved music volume when the menu starts.
        LoadMusicPreference();
    }

    public void PlayGame()
    {
        // Reset Souls before starting a new game.
        PlayerPrefs.SetInt("Souls", 0);
        PlayerPrefs.Save();

        SceneManager.LoadScene(1);
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Main_Menu()
    {
        SceneManager.LoadScene(0);
    }

    public void Credits_Button()
    {
        SceneManager.LoadScene(6);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private void LoadMusicPreference()
    {
        // Create the preference if one does not already exist.
        if (!PlayerPrefs.HasKey("musicVolume"))
        {
            PlayerPrefs.SetFloat("musicVolume", 0.5f);
            PlayerPrefs.Save();
        }

        // Only attempt to change the slider if one has been assigned.
        if (musicSlider != null)
        {
            musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
        }
        else
        {
            Debug.LogWarning(
                "MainMenu: Music Slider has not been assigned in the Inspector."
            );
        }
    }
}