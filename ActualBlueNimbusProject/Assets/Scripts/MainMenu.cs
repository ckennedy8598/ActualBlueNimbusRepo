/*
 * ****************************************************************************** *
 * Last Modified: Bobby Lapadula                                                  *
 * Date and Time: 2/8/2024 02:11                                                  *
 * ****************************************************************************** *
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuiteGame()
    {
        Application.Quit();
    }
}
