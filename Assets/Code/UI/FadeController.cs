using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeController : MonoBehaviour 
{
    public Image fadeImage;  // The black image that will fade in/out
    public float fadeDuration = 1f;  // Time for fading in/out

    private void Start() 
    {
        // Ensure the fade image starts as completely transparent
        Color color = fadeImage.color;
        color.a = 0f; // Set alpha to 0
        fadeImage.color = color;
        fadeImage.gameObject.SetActive(true); // Make sure the image is active
    }

    public void FadeIn() 
    {
        StartCoroutine(Fade(0f, 1f));  // Fade to black
    }

    public void FadeOut() 
    {
        StartCoroutine(Fade(1f, 0f));  // Fade from black
    }

    private IEnumerator Fade(float startAlpha, float endAlpha) 
    {
        float elapsedTime = 0f;
        Color fadeColor = fadeImage.color;

        // Set the image to active before starting the fade
        fadeImage.gameObject.SetActive(true);

        while (elapsedTime < fadeDuration) 
        {
            elapsedTime += Time.deltaTime;
            fadeColor.a = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / fadeDuration);
            fadeImage.color = fadeColor;
            yield return null;
        }

        fadeColor.a = endAlpha;
        fadeImage.color = fadeColor;

        // Deactivate the image if we're fading out to make it invisible
        if (endAlpha == 0f) 
        {
            fadeImage.gameObject.SetActive(false);
        }
    }
}

