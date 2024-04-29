using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsEnder : MonoBehaviour
{
    [SerializeField] private AudioSource music;
    void Start()
    {
        music.Play();
        StartCoroutine(EndScene());
    }

    private IEnumerator EndScene()
    {
        yield return new WaitForSeconds(50);
        SceneManager.LoadScene(0);
    }
}
