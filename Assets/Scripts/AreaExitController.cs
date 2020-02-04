using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AreaExitController : MonoBehaviour
{
    public AreaEntranceController entrance;

    [Header("Exiting attributes")]
    public string nextArea;
    public string areaTransitionName;
    public float waitToLoad = 1F;

    private bool shouldLoadWithFade;

    private void Start()
    {
        entrance.areaTransitionName = areaTransitionName;
    }

    private void Update()
    {
        if (shouldLoadWithFade)
        {
            // needs same time on every machine
            waitToLoad -= Time.deltaTime;
            if(waitToLoad <= 0)
            {
                shouldLoadWithFade = false;
                SceneManager.LoadScene(nextArea);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Exit to " + nextArea);
        if (collision.tag.Equals("Player"))
        {
            shouldLoadWithFade = true;
            GameManager.GM.fading = true;
            FadeController.fader.FadeToBlack();

            PlayerController.player.areaTransitionName = areaTransitionName;
        }
    }
}
