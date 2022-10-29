using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventoryItem 
{
    public ScriptableObject_SkillingItems skillingItem;
    public int stackSize;

    public InventoryItem(ScriptableObject_SkillingItems item)
    {
        skillingItem = item;
        AddToStack();
    }

    public void AddToStack()
    {
        stackSize = skillingItem.quanity;
    }

    public void RemoveFromStack()
    {
        stackSize--;
    }

}
