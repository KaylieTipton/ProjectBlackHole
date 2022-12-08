using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryUIMenu : MonoBehaviour
{
    public GameObject inventorySlot;
    public Inventory inventory;

    public Button inventoryButton;
    public GameObject inventoryInfoBox;

    public List<GameObject> slotList;
    private TMP_Text infoName;
    private Image infoImage;
    private TMP_Text infoDescrip;

    public void Awake()
    {
        infoName = inventoryInfoBox.GetComponent<InventoryBox>().itemName;
        infoImage = inventoryInfoBox.GetComponent<InventoryBox>().itemImage;
        infoDescrip = inventoryInfoBox.GetComponent<InventoryBox>().itemDescription;
        infoName.text = "";
        infoImage.sprite = null;
        infoDescrip.text = "";
    }

    // Runs through the entire Inventory List
    // If the Item is not of the Type BUILDING then it creates an inventory slot
    public void BuildInventoryUI()
    {
        for (int i = 0; i < inventory.inventoryList.Count; i++)
        {
            if (inventory.inventoryList[i].skillingItem.itemType != ITEMTYPE.BUILDING)
            {
                MakeSlot(inventorySlot, inventory.inventoryList[i]);
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
        infoName.text = "";
        infoImage.sprite = null;
        infoDescrip.text = "";


    }

    // Make the inventory slot using the item information
    public void MakeSlot(GameObject _inventorySlot, InventoryItem _inventoryItem)
    {
        _inventorySlot.GetComponent<InventorySlot>().amountText.text = _inventoryItem.stackSize + "";
        _inventorySlot.GetComponent<InventorySlot>().invItemIcon.sprite = _inventoryItem.skillingItem.invIcon;
        _inventorySlot.GetComponent<InventorySlot>().invItem = _inventoryItem.skillingItem;
        _inventorySlot.GetComponent<InventorySlot>().index = slotList.Count;

        slotList.Add(Instantiate(_inventorySlot, transform));
        
        int index = _inventorySlot.GetComponent<InventorySlot>().index;
        inventoryButton = slotList[slotList.Count - 1].GetComponent<Button>();
        inventoryButton.onClick.AddListener(() => DisplayInfo(index));
    }

    public void DisplayInfo(int _index)
    {
        Debug.Log(_index);
        infoName.text = slotList[_index].GetComponent<InventorySlot>().invItem.resourceName;
        infoImage.sprite = slotList[_index].GetComponent<InventorySlot>().invItem.invIcon;
        infoDescrip.text = slotList[_index].GetComponent<InventorySlot>().invItem.itemDescription;

    }


}
