using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    [Header("Item Details")]
    public Sprite sprite;
    public string itemName;
    public string description;

    [Header("Item Type")]
    public bool isItem;
    public bool isWeapon;
    public bool isArmor;

    [Header("Item Stats")]
    public int worth;
    public int amountToChange;
    public bool affectHealth, affectMana, affectStrength;

    [Header("Weapon/Armor Details")]
    public int weaponStrength;
    public int armorStrength;

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
