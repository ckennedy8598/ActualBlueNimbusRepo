/*
 * ****************************************************************************** *
 * Created by Bobby Lapadula                                                      *
 * Last Modified by Bobby Lapadula                                                *
 * Date and Time: 3/15/2024 14:16                                                 *
 *                                                                                *
 * This is the end level script. It is primarily a test script for winning a      *
 * level. It displays "You Win" text upon colliding with an object then goes to   *
 * the main menu after 5 seconds.                                                 *
 * ****************************************************************************** *
*/
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class levelEnd : MonoBehaviour
{
    [SerializeField]
    public TMP_Text WinText;
    public Game_Master resetCheckPoint;

    private void Start()
    {
        WinText.enabled = false;
    }

    // Displays the win text for 5 seconds before loading the next level
    private IEnumerator OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            WinText.enabled = true;
            yield return new WaitForSeconds(5);
            WinText.enabled = false;
<<<<<<< Updated upstream
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
=======
            
           SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            resetCheckPoint = GameObject.FindGameObjectWithTag("GM").GetComponent<Game_Master>();
            resetCheckPoint.lastCheckpointPos = new Vector2(26, -3);
>>>>>>> Stashed changes
        }
    }
}
