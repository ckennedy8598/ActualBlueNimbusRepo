/*
 * ****************************************************************************** *
 * Created by Bobby Lapadula                                                      *
 * Last Modified by Chris Bunnell                                                 *
 * Date and Time: 3/29/2024 1:42 PM                                               *
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
            Application.Quit();
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
