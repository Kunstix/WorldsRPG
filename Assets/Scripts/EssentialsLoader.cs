using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EssentialsLoader : MonoBehaviour
{
    public GameObject player;
    public GameObject uiScreen;
    public GameObject manager;

    void Start()
    {
        if (PlayerController.player == null)
        {
            PlayerController clone = Instantiate(player).GetComponent<PlayerController>();
            PlayerController.player = clone;
        }

        if (FadeController.fader == null)
        {
            FadeController clone = Instantiate(uiScreen).GetComponent<FadeController>();
            FadeController.fader = clone;
        }

        if (GameManager.GM == null)
        {
            Instantiate(manager).GetComponent<GameManager>();
        }
    }
}
