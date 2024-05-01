using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] TMP_Text Volume;
    [SerializeField] public Button Resume;
    [SerializeField] public Button Options;
    [SerializeField] public Button MainMenu;
    [SerializeField] public Button Quit;

    [SerializeField] public GameObject optionsMenu;
    [SerializeField] public GameObject controlsMenu;

    public bool paused = false;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            pauseMenu();
        }
    }

    private void pauseMenu()
    {
        if (!paused)
        {
            paused = true;
            PauseGame();
            setButtonsActive();
        }
        else
        {
            paused = false;
            ResumeGame();
            setButtonsDeactive();
            setControlsOff();
            optionsTestOff();
        }
    }

    public void setButtonsActive()
    {
        Resume.gameObject.SetActive(true);
        Options.gameObject.SetActive(true);
        MainMenu.gameObject.SetActive(true);
        Quit.gameObject.SetActive(true);
    }
    public void setButtonsDeactive()
    {
        Resume.gameObject.SetActive(false);
        Options.gameObject.SetActive(false);
        MainMenu.gameObject.SetActive(false);
        Quit.gameObject.SetActive(false);
    }

    public void setControlsOn()
    {
        controlsMenu.SetActive(true);
    }

    public void setControlsOff()
    {
        controlsMenu.SetActive(false);
    }

    public void optionsTestOn()
    {
        optionsMenu.SetActive(true);
    }

    public void optionsTestOff()
    {
        optionsMenu.SetActive(false);
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
    }

    public void setIsPaused()
    {
        paused = false;
    }

    public bool getIsPaused()
    {
        return paused;
    }
}
