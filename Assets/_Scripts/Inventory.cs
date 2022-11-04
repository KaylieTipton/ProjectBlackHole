using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<InventoryItem> inventoryList;
    private Dictionary<ScriptableObject_SkillingItems, InventoryItem> itemDictionary = new Dictionary<ScriptableObject_SkillingItems, InventoryItem>();

    public void AddItem(ScriptableObject_SkillingItems skillingItem)
    {
        Debug.Log("Here");
        if(itemDictionary.TryGetValue(skillingItem, out InventoryItem item))
        {  
            item.AddToStack();
            Debug.Log($"{item.skillingItem.resourceName} total stack is now {item.stackSize}");
        }
        else
        {
            InventoryItem newItem = new InventoryItem(skillingItem);
            inventoryList.Add(newItem);
            itemDictionary.Add(skillingItem, newItem);
            Debug.Log($"Added {skillingItem.resourceName} to the inventoryList for the first time.");
        }
    }

    public void RemoveItem(ScriptableObject_SkillingItems skillingItem)
    {
        if(itemDictionary.TryGetValue(skillingItem, out InventoryItem item))
        {
            item.RemoveFromStack();
            if(item.stackSize == 0)
            {
                inventoryList.Remove(item);
                itemDictionary.Remove(skillingItem);
            }
        }
    }
}
