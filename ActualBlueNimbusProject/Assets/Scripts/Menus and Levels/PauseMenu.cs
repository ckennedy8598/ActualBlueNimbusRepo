using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] public Button Resume;
    [SerializeField] public Button Options;
    [SerializeField] public Button MainMenu;
    [SerializeField] public Button Quit;
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
        }
    }

    private void setButtonsActive()
    {
        Resume.gameObject.SetActive(true);
        Options.gameObject.SetActive(true);
        MainMenu.gameObject.SetActive(true);
        Quit.gameObject.SetActive(true);
    }
    private void setButtonsDeactive()
    {
        Resume.gameObject.SetActive(false);
        Options.gameObject.SetActive(false);
        MainMenu.gameObject.SetActive(false);
        Quit.gameObject.SetActive(false);
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(1);
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
}