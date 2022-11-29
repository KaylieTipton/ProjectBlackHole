using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

[System.Serializable]
public enum AXETYPE
{
    STONE = 88,
    IRON = 89
}

[System.Serializable]
public class UIWoodcuttingState : UIState
{
    public ScriptableObject_SkillingItems axe;
    // List of all the buttons found in the Woodcutting Menu
    public List<ButtonSkillingItemPairs> buttonList;
    public override void OnStart()
    {
        base.OnStart();
        FindAxe(SkillData.instance.inventory);
    }

    public override void UpdateState(float dt) //DT is deltatime
    {
        base.UpdateState(dt);

        CalculateUnlock();
    }

    public override void OnExit()
    {
        base.OnExit();
    }

// DO FOR ALL SKILL STATES
// Calculates if the buttons are unlocked and interactable or if they should display the locked image
    public void CalculateUnlock() {
        foreach(ButtonSkillingItemPairs bsPairs in buttonList)
        {
            bsPairs.button.interactable = bsPairs.skillingItem.isUnlocked;
            bsPairs.button.GetComponent<SkillItemMenu>().lockedImage.SetActive(!bsPairs.skillingItem.isUnlocked);
        }
    }

    // Finds the current AXE that the player has so that skill data can use the info for upgrade information
    public void FindAxe(Inventory _inventory)
    {
        List<InventoryItem> inv = _inventory.inventoryList.Where(item => item.skillingItem.itemType == ITEMTYPE.AXE).ToList();
        if(inv.Count > 0) {
            axe = inv[0].skillingItem;
            return;
        }
    }
}


[System.Serializable]
public class ButtonSkillingItemPairs
{
    public Button button;
    public ScriptableObject_SkillingItems skillingItem;

}
