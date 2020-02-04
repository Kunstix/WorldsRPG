using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Header("Player Level")]
    public string playerName;
    public int playerLevel = 1;
    public int maxLevel;
    public int currentXp;
    public int[] xpToNextLevel;
    public int baseXp = 1000;

    [Header("Player Stats")]
    public int currentHealth;
    public int maxHealth = 100;
    public int currentMana;
    public int maxMana = 30;
    public int[] manaForLevel;
    public int strength;
    public int defense;
    public int weaponPower;
    public int armorPower;
    public string currentWeapon;
    public string currentArmor;

    public Sprite playerSprite;


    void Start()
    {
        // Initiate needed xp for different levels
        xpToNextLevel = new int[maxLevel];
        xpToNextLevel[1] = baseXp;
        for(int level = 2; level < xpToNextLevel.Length; level++)
        {
            xpToNextLevel[level] = Mathf.RoundToInt(xpToNextLevel[level - 1] * 1.05F);
        }
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Backspace)){
            AddXp(500);
        }
    }

    public void AddXp(int addedXp)
    {
        currentXp += addedXp;
        if(playerLevel < maxLevel)
        {
            if (currentXp > xpToNextLevel[playerLevel] && playerLevel < maxLevel)
            {
                // increase playerlevel
                currentXp -= xpToNextLevel[playerLevel];
                playerLevel++;
                Debug.Log("Level Up! " + playerLevel);

                IncreaseStrengthOrDefense();
                IncreaseHealth();
                IncreaseMana();

            }
        } 

        if(playerLevel >= maxLevel)
        {
            currentXp = 0;
        }
    }

    private void IncreaseStrengthOrDefense()
    {
        if (playerLevel % 2 == 0)
        {
            strength++;
        }
        else
        {
            defense++;
        }
    }

    private void IncreaseHealth()
    {
        maxHealth = Mathf.RoundToInt(maxHealth * 1.1F);
        currentHealth = maxHealth;
    }

    private void IncreaseMana()
    {
        maxMana += manaForLevel[playerLevel];
        currentMana = maxMana;
    }
}
