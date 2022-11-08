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

    public void BuildInventoryUI()
    {
        for(int i = 0; i < inventory.inventoryList.Count; i++){
            MakeSlot(inventorySlot, inventory.inventoryList[i]);
        }
    }

    public void KillAllChildern()
    {
        foreach(Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        slotList.Clear();
    }

    public void MakeSlot(GameObject _inventorySlot, InventoryItem _inventoryItem)
    {
        _inventorySlot.GetComponent<InventorySlot>().amountText.text = _inventoryItem.skillingItem.quanity + "";
        _inventorySlot.GetComponent<InventorySlot>().invItemIcon.sprite = _inventoryItem.skillingItem.invIcon;

        slotList.Add(Instantiate(_inventorySlot, transform));
    }

    
}
