using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUIMenu : MonoBehaviour
{
    public GameObject inventorySlot;
    public Inventory inventory;

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
    }

    public void MakeSlot(GameObject _inventorySlot, InventoryItem _inventoryItem)
    {

        Instantiate(_inventorySlot, transform);
    }
}
