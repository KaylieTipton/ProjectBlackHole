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
        AddToStack(1);
    }

    public void AddToStack(int _amount)
    {
        stackSize += _amount;
    }

    public void RemoveFromStack()
    {
        stackSize--;
    }
    
    public void RemoveFromStack(int _amount)
    {
        stackSize -= _amount;
        skillingItem.quanity = stackSize;
    }

}
