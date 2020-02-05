using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public GameObject menu;
    public GameObject[] windows;

    private PlayerStats[] playerStats;

    public Text[] nameTexts, healthTexts, manaTexts, lvlTexts, xpTexts;
    public Slider[] xpSliders;
    public Image[] playerImages;
    public GameObject[] statsHolder;
 
    void Start()
    {
        
    }

    void Update()
    {
     if(Input.GetKeyDown(KeyCode.Space))
        {
            if (menu.activeInHierarchy)
            {
                CloseMenu();
            } else
            {
                OpenMenu();
            }
        }
    }

    public void UpdateStats()
    {
        playerStats = GameManager.GM.playerStats;

        for(int i = 0; i < playerStats.Length; i++)
        {
            if (playerStats[i].gameObject.activeInHierarchy)
            {
                statsHolder[i].SetActive(true);
                nameTexts[i].text = playerStats[i].playerName;
                healthTexts[i].text = "HP: " + playerStats[i].currentHealth + "/" + playerStats[i].maxHealth;
                manaTexts[i].text = "MP: " + playerStats[i].currentMana + "/" + playerStats[i].maxMana;
                lvlTexts[i].text = "Lvl: " + playerStats[i].playerLevel;
                xpTexts[i].text = "" + playerStats[i].currentXp + "/" + playerStats[i].xpToNextLevel[playerStats[i].playerLevel];
                xpSliders[i].maxValue = playerStats[i].xpToNextLevel[playerStats[i].playerLevel];
                xpSliders[i].value = playerStats[i].currentXp;
                playerImages[i].sprite = playerStats[i].playerSprite;
            } else
            {
                statsHolder[i].SetActive(false);
            }
        }
    }


    public void ToggleWindow(int windowNumber)
    {
        for(int i = 0; i < windows.Length; i++)
        {
            if(i == windowNumber)
            {
                windows[i].SetActive(!windows[i].activeInHierarchy);
            } else
            {
                windows[i].SetActive(false);
            }
        }
    }

    private void OpenMenu()
    {
        menu.SetActive(true);
        UpdateStats();
        GameManager.GM.menuOpen = true;
    }

    public void CloseMenu()
    {
        for(int i = 0; i < windows.Length; i++)
        {
            windows[i].SetActive(false);
        }

        menu.SetActive(false);
        GameManager.GM.menuOpen = false;
    }
}
