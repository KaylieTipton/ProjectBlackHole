using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

// Second verse Same as the first

// Most of this is going to be scrapped to rebuild the Town menu since it will no longer be instanced prefabs like the inventory so to make the shop thingy easier

public class TownUIMenu : MonoBehaviour
{
    public Inventory shopInventory;
    public TMP_Text itemName;
    public Image itemImage;
    public TMP_Text itemDescription;
    public TMP_Text itemQuanity;

    public ScriptableObject_SkillingItems building;

    public void Start()
    {
        DisplayBuildingInfo();
    }

    public void DisplayBuildingInfo()
    {
        itemName.text = building.resourceName;
        itemImage.sprite = building.invIcon;
        itemDescription.text = building.itemDescription;
        /*if (SkillData.instance.inventory.inventoryList.Count > 0)
        {
            for (int i = 0; i < SkillData.instance.inventory.inventoryList.Count; i++)
            {
                if (building.ID == SkillData.instance.inventory.inventoryList[i].skillingItem.ID)
                {
                    itemQuanity.text = SkillData.instance.inventory.inventoryList[i].stackSize + "";
                }
            }
        }
        else{
            itemQuanity.text = "0";
        }*/
        itemQuanity.text = "HEllo";

    }


}
