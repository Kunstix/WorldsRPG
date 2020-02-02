using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeController : MonoBehaviour
{
    public static FadeController fader;

    public Image fadeScreen;

    public float fadeSpeed = 1F;

    private bool shouldFadeToBlack;
    private bool shouldFadeFromBlack;

    void Start()
    {
        if (fader == null)
        {
            fader = this;
        }
        else
        {
            if(fader != this)
            {
            Destroy(gameObject);
            }
        }
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        // Screen fade to black black
        if (shouldFadeToBlack)
        {
            Debug.Log("Fading to black...");
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b,
                                         Mathf.MoveTowards(fadeScreen.color.a, 1F, fadeSpeed * Time.deltaTime));
            if(fadeScreen.color.a == 1F)
            {
                shouldFadeToBlack = false;
            }
        }

        // Screen's black should fade
        if (shouldFadeFromBlack)
        {
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b,
                                         Mathf.MoveTowards(fadeScreen.color.a, 0F, fadeSpeed * Time.deltaTime));
            if (fadeScreen.color.a == 1F)
            {
                shouldFadeFromBlack = false;
            }
        }
    }

    public void FadeToBlack()
    {
        Debug.Log("Fade to black.");
        shouldFadeToBlack = true;
        shouldFadeFromBlack = false;
    }

    public void FadeFromBlack()
    {
        Debug.Log("Fade from black.");
        shouldFadeFromBlack = true;
        shouldFadeToBlack = false;
    }
}
