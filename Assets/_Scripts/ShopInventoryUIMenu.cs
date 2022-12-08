using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class ShopInventoryUIMenu : MonoBehaviour
{
    public GameObject shopSlot;
    public Inventory shopInventory;

    public List<GameObject> slotList;

    private ITEMTYPE itemType;
    private ScriptableObject_SkillingItems shopItem;
    private Button shopButton;

    public void Start()
    {

    }

    public void SetInventory(GameObject _shopInventory)
    {
        Debug.Log("Set");
        shopInventory = _shopInventory.GetComponent<Inventory>();
    }



   
    // Runs through the entire Inventory List
    // If the Item is not of the Type BUILDING then it creates an inventory slot
    public void BuildInventoryUI()
    {
        for (int i = 0; i < shopInventory.inventoryList.Count; i++)
        {
            if (shopInventory.inventoryList[i].skillingItem.itemType != ITEMTYPE.BUILDING)
            {
                MakeSlot(shopSlot, shopInventory.inventoryList[i]);
            }

        }
    }

    // DESTROY ALL THE YOUNGLINGS UPON CLOSURE
    public void KillAllChildern()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        slotList.Clear();
    }

    // Make the inventory slot using the item information
    public void MakeSlot(GameObject _shopSlot, InventoryItem _inventoryItem)
    {
        _shopSlot.GetComponent<ShopSlot>().itemName.text = _inventoryItem.skillingItem.nodeName + "";
        _shopSlot.GetComponent<ShopSlot>().itemImage.sprite = _inventoryItem.skillingItem.invIcon;
        _shopSlot.GetComponent<ShopSlot>().shopItem = _inventoryItem.skillingItem;
        _shopSlot.GetComponent<ShopSlot>().index = slotList.Count;

        int index = _shopSlot.GetComponent<ShopSlot>().index;

        slotList.Add(Instantiate(_shopSlot, transform));
        shopButton = slotList[slotList.Count - 1].GetComponent<Button>();
        shopButton.onClick.AddListener(() => Buy(index));
    }

    public void Buy(int _index)
    {

        if (slotList[_index].GetComponent<ShopSlot>().shopItem is ScriptableObject_UpgradeTools)
        {
            for (int i = 0; i < SkillData.instance.inventory.inventoryList.Count; i++)
            {
                if (SkillData.instance.inventory.inventoryList[i].skillingItem is ScriptableObject_UpgradeTools)
                {
                    itemType = SkillData.instance.inventory.inventoryList[i].skillingItem.itemType;
                    switch (itemType)
                    {
                        case ITEMTYPE.AXE:
                            if(slotList[_index].GetComponent<ShopSlot>().shopItem.itemType != ITEMTYPE.AXE)
                                break;
                            AxeItem(i, _index);
                            Debug.Log("AXE");
                            return;
                    }
                }
            }
        }
        else
        {
            Debug.Log("GIVE PLAYER ITEM");
        }
    }



     public void AxeItem(int _invIndex, int _shopIndex)
    {
        Debug.Log("AXE@");
        if (SkillData.instance.inventory.inventoryList[_invIndex].skillingItem.itemType == ITEMTYPE.AXE)
        {
            SkillData.instance.inventory.RemoveItem(SkillData.instance.inventory.inventoryList[_invIndex].skillingItem);
            shopItem = slotList[_shopIndex].GetComponent<ShopSlot>().shopItem;
            shopInventory.RemoveItem(shopItem);
            SkillData.instance.inventory.AddItem(shopItem);
        }

    }

}
