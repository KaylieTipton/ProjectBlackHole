using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownUIMenu : MonoBehaviour
{
    public GameObject townSlot;
    public Inventory inventory;

    public List<GameObject> slotList;

    public void BuildInventoryUI()
    {
        for(int i = 0; i < inventory.inventoryList.Count; i++){
            if(inventory.inventoryList[i].skillingItem.skillType == CurrentSkill.Building)
            {
                MakeSlot(townSlot, inventory.inventoryList[i]);
            }
            
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

    public void MakeSlot(GameObject _townSlot, InventoryItem _inventoryItem)
    {
        _townSlot.GetComponent<TownSlot>().itemName.text = _inventoryItem.skillingItem.resourceName + "";
        _townSlot.GetComponent<TownSlot>().itemImage.sprite = _inventoryItem.skillingItem.invIcon;
        _townSlot.GetComponent<TownSlot>().itemDescription.text = _inventoryItem.skillingItem.itemDescription + "";
        _townSlot.GetComponent<TownSlot>().itemQuanity.text = _inventoryItem.skillingItem.quanity + "";


        slotList.Add(Instantiate(_townSlot, transform));
    }

    
}
