using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [Header("Menu Items")]
    public GameObject menu;
    public GameObject[] windows;

    [Header("Player General Stats")]
    private PlayerStats[] playerStats;

    [Header("Player General Status Fields")]
    public Text[] nameTexts, healthTexts, manaTexts, lvlTexts, xpTexts;
    public Slider[] xpSliders;
    public Image[] playerImages;

    [Header("Status Container for General Status Fields")]
    public GameObject[] statsHolder;

    [Header("Player Buttons")]
    public GameObject[] statusButtons;

    [Header("Player Details Stats")]
    public Text statusName, statusHealth, statusMana, statusStrength, statusDefense, statusWeapon, statusWeaponPwr, statusArmor, statusArmorPwr, statusXp;
    public Image playerImg;

    public ItemButtonController[] itemButtons;

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (menu.activeInHierarchy)
            {
                CloseMenu();
            }
            else
            {
                OpenMenu();
            }
        }
    }

    public void UpdateStats()
    {
        playerStats = GameManager.GM.playerStats;

        for (int i = 0; i < playerStats.Length; i++)
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
            }
            else
            {
                statsHolder[i].SetActive(false);
            }
        }
    }


    public void ToggleWindow(int windowNumber)
    {
        UpdateStats();

        for (int i = 0; i < windows.Length; i++)
        {
            if (i == windowNumber)
            {
                windows[i].SetActive(!windows[i].activeInHierarchy);
            }
            else
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
        for (int i = 0; i < windows.Length; i++)
        {
            windows[i].SetActive(false);
        }

        menu.SetActive(false);
        GameManager.GM.menuOpen = false;
    }

    public void OpenStatus()
    {
        UpdateStats();

        UpdatePlayerStats(0);

        Debug.Log("Open Stats.");
        for (int i = 0; i < statusButtons.Length; i++)
        {
           statusButtons[i].SetActive(playerStats[i].gameObject.activeInHierarchy);
           statusButtons[i].GetComponentInChildren<Text>().text = playerStats[i].playerName;
        }
    }

    public void UpdatePlayerStats(int selected)
    {
        statusName.text = playerStats[selected].playerName;
        statusHealth.text = "" + playerStats[selected].currentHealth + "/" + playerStats[selected].maxHealth;
        statusMana.text = "" + playerStats[selected].currentMana + "/" + playerStats[selected].maxMana;
        statusStrength.text = "" + playerStats[selected].strength.ToString();
        statusDefense.text = "" + playerStats[selected].defense.ToString();
        if(playerStats[selected].currentArmor != "")
        {
           statusWeapon.text = playerStats[selected].currentWeapon.ToString();
        }
        statusWeaponPwr.text = playerStats[selected].weaponPower.ToString();
        if (playerStats[selected].currentArmor != "")
        {
           statusArmor.text = playerStats[selected].currentArmor.ToString();
        }
        statusArmorPwr.text = playerStats[selected].armorPower.ToString();
        statusXp.text = (playerStats[selected].xpToNextLevel[playerStats[selected].playerLevel] - playerStats[selected].currentXp).ToString();
        playerImg.sprite = playerStats[selected].playerSprite;
    }

    public void ShowItems()
    {
        Debug.Log("Show items.");
        for(int i = 0; i < itemButtons.Length; i++)
        {
            itemButtons[i].buttonValue = i;

            if(GameManager.GM.itemsOwned[i] != "")
            {
                itemButtons[i].buttonImage.gameObject.SetActive(true);
                itemButtons[i].buttonImage.sprite = GameManager.GM.GetItemDetails(GameManager.GM.itemsOwned[i]).sprite;
                itemButtons[i].amount.text = GameManager.GM.numberOfItems[i].ToString();
            } else
            {
                itemButtons[i].buttonImage.gameObject.SetActive(false);
                itemButtons[i].amount.text = "";
            }
        }
    }
}
