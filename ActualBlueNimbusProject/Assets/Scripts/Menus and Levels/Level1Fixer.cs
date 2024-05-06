using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1Fixer : MonoBehaviour
{
    public PauseMenu pauseMenu;
    void Start()
    {
        pauseMenu = FindObjectOfType<PauseMenu>();
        pauseMenu.paused = false;
        Debug.Log("This is isPaued: " +  pauseMenu.getIsPaused());
        Time.timeScale = 1.0f;
    }
}
