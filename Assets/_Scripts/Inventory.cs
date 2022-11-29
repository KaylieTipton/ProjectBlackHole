using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<InventoryItem> inventoryList;
    private Dictionary<ScriptableObject_SkillingItems, InventoryItem> itemDictionary = new Dictionary<ScriptableObject_SkillingItems, InventoryItem>();

    // Adds a single item to the Inventory. If the item is already in the inventory List then it just adds 1 to the current stack
    // If the item is not in the inventory list then it adds the item to the inventory list as a new item
    // It sorts the list by ID everytime something new is added
    public void AddItem(ScriptableObject_SkillingItems skillingItem)
    {
        Debug.Log("Here");
        if (itemDictionary.TryGetValue(skillingItem, out InventoryItem item))
        {
            item.AddToStack(1);
            Debug.Log($"{item.skillingItem.resourceName} total stack is now {item.stackSize}");
        }
        else
        {
            InventoryItem newItem = new InventoryItem(skillingItem);
            inventoryList.Add(newItem);
            itemDictionary.Add(skillingItem, newItem);
            if (inventoryList.Count > 1)
                inventoryList = inventoryList.OrderBy(i => i.skillingItem.ID).ToList();
            Debug.Log($"Added {skillingItem.resourceName} to the inventoryList for the first time.");
        }


    }


    // Adds a single item to the Inventory. If the item is already in the inventory List then it adds the gathered amount to the current stack
    // If the item is not in the inventory list then it adds the item to the inventory list as a new item
    // It sorts the list by ID everytime something new is added

    // Possible Problem: It may not add an item based on the gather multiplier the first time it adds....Need to investigate
    public void AddItem(ScriptableObject_SkillingItems skillingItem, int _gatherMultiplier)
    {
        Debug.Log("Here");
        if (itemDictionary.TryGetValue(skillingItem, out InventoryItem item))
        {
            item.AddToStack(_gatherMultiplier);
            Debug.Log($"{item.skillingItem.resourceName} total stack is now {item.stackSize}");
        }
        else
        {
            InventoryItem newItem = new InventoryItem(skillingItem);
            inventoryList.Add(newItem);
            itemDictionary.Add(skillingItem, newItem);
            if (inventoryList.Count > 1)
                inventoryList = inventoryList.OrderBy(i => i.skillingItem.ID).ToList();
            Debug.Log($"Added {skillingItem.resourceName} to the inventoryList for the first time.");
        }


    }

    // Removes 1 from the item stack
    // If the stack is empty then it removes the item from the list and the dictionary

    public void RemoveItem(ScriptableObject_SkillingItems skillingItem)
    {
        if (itemDictionary.TryGetValue(skillingItem, out InventoryItem item))
        {
            item.RemoveFromStack();
            if (item.stackSize == 0)
            {
                inventoryList.Remove(item);
                itemDictionary.Remove(skillingItem);
            }
        }
    }

    // Removes _amount from the item stack
    // If the stack is empty then it removes the item from the list and the dictionary
    public void RemoveItem(ScriptableObject_SkillingItems skillingItem, int _amount)
    {
        if (itemDictionary.TryGetValue(skillingItem, out InventoryItem item))
        {
            item.RemoveFromStack(_amount);
            if (item.stackSize == 0)
            {
                inventoryList.Remove(item);
                itemDictionary.Remove(skillingItem);
            }
        }
    }
}
