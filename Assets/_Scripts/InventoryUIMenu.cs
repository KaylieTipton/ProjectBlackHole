using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryUIMenu : MonoBehaviour
{
    public GameObject inventorySlot;
    public Inventory inventory;

    public List<GameObject> slotList;

    // Runs through the entire Inventory List
    // If the Item is not of the Type BUILDING then it creates an inventory slot
    public void BuildInventoryUI()
    {
        for(int i = 0; i < inventory.inventoryList.Count; i++){
            if(inventory.inventoryList[i].skillingItem.itemType != ITEMTYPE.BUILDING)
            {
                MakeSlot(inventorySlot, inventory.inventoryList[i]);
            }
            
        }
    }

    // DESTROY ALL THE YOUNGLINGS UPON CLOSURE
    public void KillAllChildern()
    {
        foreach(Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        slotList.Clear();
    }

    // Make the inventory slot using the item information
    public void MakeSlot(GameObject _inventorySlot, InventoryItem _inventoryItem)
    {
        _inventorySlot.GetComponent<InventorySlot>().amountText.text = _inventoryItem.stackSize + "";
        _inventorySlot.GetComponent<InventorySlot>().invItemIcon.sprite = _inventoryItem.skillingItem.invIcon;

        slotList.Add(Instantiate(_inventorySlot, transform));
    }

    
}
