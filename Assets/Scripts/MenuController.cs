using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public GameObject menu;

    void Start()
    {
        
    }

    void Update()
    {
     if(Input.GetKeyDown(KeyCode.Space))
        {
            if (menu.activeInHierarchy)
            {
                menu.SetActive(false);
                GameManager.GM.menuOpen = false;
            } else
            {
                menu.SetActive(true);
                GameManager.GM.menuOpen = true;
            }
        }
    }
}
