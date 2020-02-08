using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager GM;

    public PlayerStats[] playerStats;

    public bool menuOpen, fading, dialogActive;

    public string[] itemsOwned;
    public int[] numberOfItems;
    public ItemController[] items;

    void Start()
    {
        if (GM == null)
        {
            GM = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (menuOpen || fading || dialogActive)
        {
            PlayerController.player.canMove = false;
        }
        else
        {
            PlayerController.player.canMove = true;
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            Debug.Log("J");
            AddItem("Steel Sword");

            RemoveItem("Health Potion");
        }
    }

    public ItemController GetItemDetails(string itemName)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i].itemName == itemName)
            {
                return items[i];
            }
        }

        return null;
    }

    public void SortItems()
    {
        bool thingsToSort = true;

        while (thingsToSort)
        {
            thingsToSort = false;
            for (int i = 0; i < itemsOwned.Length - 1; i++)
            {
                // check if item slot is empty, if so grap the next slot and move it forward
                if (itemsOwned[i] == "")
                {
                    itemsOwned[i] = itemsOwned[i + 1];
                    itemsOwned[i + 1] = "";

                    numberOfItems[i] = numberOfItems[i + 1];
                    numberOfItems[i + 1] = 0;

                    if (itemsOwned[i] != "")
                    {
                        // if there was an item moved, we have new empty space, keep sorting
                        thingsToSort = true;
                    }
                }
            }
        }

    }

    public void AddItem(string itemToAdd)
    {
        bool foundPosition = false;

        // find position for item in inventory
        int newItemPosition = FindNewItemPosition(itemToAdd, ref foundPosition);

        Debug.Log("New position: " + newItemPosition);
        if (foundPosition)
        {
            // only if the item exists in the game
            bool itemExists = CheckIfItemExists(itemToAdd);

            // add item to the inventory
            if (itemExists)
            {
                itemsOwned[newItemPosition] = itemToAdd;
                numberOfItems[newItemPosition]++;
            }
            else
            {
                Debug.LogError(itemToAdd + " doesn't exist.");
            }

            MenuController.menu.ShowItems();
        }
    }

    public void RemoveItem(string itemToRemove)
    {
        Debug.Log("Remove item: " + itemToRemove);

        bool foundItem = false;
        int itemPosition = 0;

        for (int i = 0; i < itemsOwned.Length; i++)
        {
            if (itemsOwned[i] == itemToRemove)
            {
                foundItem = true;
                itemPosition = i;
                i = itemsOwned.Length;
                Debug.Log("Found item at position: " + itemPosition);
            }
        }

        if (foundItem)
        {
            numberOfItems[itemPosition]--;
            if (numberOfItems[itemPosition] <= 0)
            {
                itemsOwned[itemPosition] = "";
            }
        }
        else
        {
            Debug.LogError("Couldn't find the item to remove");
        }

        MenuController.menu.ShowItems();
    }

    private int FindNewItemPosition(string itemToAdd, ref bool foundPosition)
    {
        int newItemPosition = 0;
        for (int i = 0; i < itemsOwned.Length; i++)
        {
            if (itemsOwned[i] == "" || itemsOwned[i] == itemToAdd)
            {
                newItemPosition = i;
                  foundPosition = true;
                i = itemsOwned.Length;
            }
        }

        return newItemPosition;
    }

    private bool CheckIfItemExists(string itemToAdd)
    {
        bool itemExists = false;
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i].itemName == itemToAdd)
            {
                itemExists = true;

                i = items.Length;
            }
        }

        Debug.Log(itemToAdd + " exists: " + itemExists);
        return itemExists;
    }
}
