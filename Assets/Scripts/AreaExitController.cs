using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AreaExitController : MonoBehaviour
{
    public AreaEntranceController entrance;

    public string nextArea;
    public string areaTransitionName;

    private void Start()
    {
        entrance.areaTransitionName = areaTransitionName;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            SceneManager.LoadScene(nextArea);
            PlayerController.player.areaTransitionName = areaTransitionName;
        }
    }
}
