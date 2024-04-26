using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsEnder : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(EndScene());
    }

    private IEnumerator EndScene()
    {
        yield return new WaitForSeconds(50);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
