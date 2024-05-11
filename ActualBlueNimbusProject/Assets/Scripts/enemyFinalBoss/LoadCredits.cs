using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadCredits : MonoBehaviour
{
    public Enemy ZephyrScript;
    // Start is called before the first frame update
    void Start()
    {
        //ZephyrScript = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (ZephyrScript == null)
        { 
            StartCoroutine(loadCredts());
        }
    }

    private IEnumerator loadCredts()
    {
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
