using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public static MenuController menu;

    [Header("Menu Items")]
    public GameObject menuPanel;
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
    public string selectedItem;
    public ItemController activeItem;
    public Text itemName, itemDescription, useButtonText;

    public GameObject itemCharChoiceMenu;
    public Text[] itemCharChoiceNames;



    void Start()
    {
        menu = this;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (menuPanel.activeInHierarchy)
            {
                CloseMenu();
            }
            else
            {
                OpenMenu();
            }
        }
    }

    private void OpenMenu()
    {
        menuPanel.SetActive(true);
        UpdatePlayerInfos();
        GameManager.GM.menuOpen = true;
    }

    public void ToggleWindow(int windowNumber)
    {
        UpdatePlayerInfos();

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

        itemCharChoiceMenu.SetActive(false);
    }

    public void CloseMenu()
    {
        for (int i = 0; i < windows.Length; i++)
        {
            windows[i].SetActive(false);
        }

        menuPanel.SetActive(false);
        GameManager.GM.menuOpen = false;

        itemCharChoiceMenu.SetActive(false);
    }

    public void OpenStatus()
    {
        UpdatePlayerInfos();

        UpdateDetailedPlayerStats(0);

        Debug.Log("Open Stats.");
        for (int i = 0; i < statusButtons.Length; i++)
        {
            // only shows playerbuttons for active players
            statusButtons[i].SetActive(playerStats[i].gameObject.activeInHierarchy);
            statusButtons[i].GetComponentInChildren<Text>().text = playerStats[i].playerName;
        }
    }

    // Updates the player overview
    public void UpdatePlayerInfos()
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

    // Grabs the character informations for a selected player and updates the single statspanel
    public void UpdateDetailedPlayerStats(int selected)
    {
        statusName.text = playerStats[selected].playerName;
        statusHealth.text = "" + playerStats[selected].currentHealth + "/" + playerStats[selected].maxHealth;
        statusMana.text = "" + playerStats[selected].currentMana + "/" + playerStats[selected].maxMana;
        statusStrength.text = "" + playerStats[selected].strength.ToString();
        statusDefense.text = "" + playerStats[selected].defense.ToString();
        if (playerStats[selected].currentArmor != "")
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
        // No empty space between items
        GameManager.GM.SortItems();

        for (int i = 0; i < itemButtons.Length; i++)
        {
            itemButtons[i].buttonValue = i;

            // grab the owned items from the GM and update the itemButtons correspondently
            if (GameManager.GM.itemsOwned[i] != "")
            {
                itemButtons[i].buttonImage.gameObject.SetActive(true);
                itemButtons[i].buttonImage.sprite = GameManager.GM.GetItemDetails(GameManager.GM.itemsOwned[i]).sprite;
                itemButtons[i].amount.text = GameManager.GM.numberOfItems[i].ToString();
                // Debug.Log(GameManager.GM.itemsOwned[i] + ":" + GameManager.GM.numberOfItems[i]);
            }
            else
            {
                itemButtons[i].buttonImage.gameObject.SetActive(false);
                itemButtons[i].amount.text = "";
            }
        }
    }

    public void UpdateSelectedItem(ItemController selectedItem)
    {
        Debug.Log("Update selected item.");
        activeItem = selectedItem;
        if (activeItem.isItem)
        {
            useButtonText.text = "Use";
        }

        if (activeItem.isWeapon || activeItem.isArmor)
        {
            useButtonText.text = "Equipp";
        }

        itemName.text = activeItem.itemName;
        itemDescription.text = activeItem.description;

    }

    public void DiscardItem()
    {
        if (activeItem != null)
        {
            GameManager.GM.RemoveItem(activeItem.itemName);
        }
    }

    public void OpenItemCharChoice()
    {
        itemCharChoiceMenu.SetActive(true);

        for (int i = 0; i < itemCharChoiceNames.Length; i++)
        {
            itemCharChoiceNames[i].text = GameManager.GM.playerStats[i].playerName;
            itemCharChoiceNames[i].transform.parent.gameObject.SetActive(GameManager.GM.playerStats[i].gameObject.activeInHierarchy);
        }
    }

    public void CloseItemCharChoice()
    {
        itemCharChoiceMenu.SetActive(false);
    }

}
