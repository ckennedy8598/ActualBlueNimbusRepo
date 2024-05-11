using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AcquireScythe : MonoBehaviour
{
    [SerializeField] Animator noScythe;
    public Player playerScript;
    // Start is called before the first frame update
    void Start()
    {
        playerScript = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("LevelEnd"))
        {
            StartCoroutine(ScytheGet());
        }
    }

    public IEnumerator ScytheGet()
    {
        playerScript.SetCanMove();
        noScythe.SetTrigger("AcquireScythe");
        yield return new WaitForSeconds(7);
        playerScript.SetCanMove();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
