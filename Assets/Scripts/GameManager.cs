using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager GM;

    public PlayerStats[] playerStats;

    public bool menuOpen, fading, dialogActive;

    void Start()
    {
        if(GM == null)
        {
            GM = this;
        } else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if(menuOpen || fading || dialogActive)
        {
            PlayerController.player.canMove = false;
        } else
        {
            PlayerController.player.canMove = true;
        }
    }
}
