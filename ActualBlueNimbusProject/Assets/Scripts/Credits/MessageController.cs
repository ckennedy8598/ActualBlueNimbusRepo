using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageController : MonoBehaviour
{
    public GameObject message;
    private bool check = false;

    // Start is called before the first frame update
    void Start()
    {
        message = GameObject.Find("Message");
    }

    // Update is called once per frame
    void Update()
    {
        if (message != null)
        {
            if (!check)
            {
                message.SetActive(true);
                check = true;
                Debug.Log("Setting Message to True.");
            }
            else
                return;
        }
    }
}
