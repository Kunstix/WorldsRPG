using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemButtonController : MonoBehaviour
{
    public Image buttonImage;
    public Text amount;
    public int buttonValue;

    public void SelectItem()
    {
        if (GameManager.GM.itemsOwned[buttonValue] != "")
        {
            Debug.Log("Itembutton hit: " + buttonValue);
            MenuController.menu.UpdateSelectedItem(GameManager.GM.GetItemDetails(GameManager.GM.itemsOwned[buttonValue]));
        }
    }
}
