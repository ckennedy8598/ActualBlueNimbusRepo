using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OpeningMessage : MonoBehaviour
{
    public TextMeshProUGUI textDisplay;
    public void Start()
    {
        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut()
    {
        float duration = 2.25f; //Fade over 3 seconds.
        float currentTime = 0f;
        Color startZero = new Color(255, 255, 255, 0);
        textDisplay.color = startZero;
        yield return new WaitForSeconds(5f);

        while (currentTime < duration)
        {
            float alpha = Mathf.Lerp(0f, 1f, currentTime / 3);
            textDisplay.color = new Color(textDisplay.color.r, textDisplay.color.g, textDisplay.color.b, alpha);
            currentTime += Time.deltaTime;
            yield return null;
        }
        
        currentTime = 0f;
        yield return new WaitForSeconds(3);

        while (currentTime < duration)
        {
            float alpha = Mathf.Lerp(1f, 0f, currentTime / 1.8f);
            textDisplay.color = new Color(textDisplay.color.r, textDisplay.color.g, textDisplay.color.b, alpha);
            currentTime += Time.deltaTime;
            yield return null;
        }
    }
}
