using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Linq;

// Contains everything relating to the active item menu.
// Displays the active item information and updates the information


public class ActiveItemMenu : MonoBehaviour
{
    public TMP_Text resourceName; 
    public Image resourceIcon;
    public TMP_Text resourceQuanity;
    public TMP_Text expText;
    public TMP_Text timeText;

    private SkillData skillData;
    public Slider progressSlider;


    private ScriptableObject_SkillingItems currentItem;
    private int foundActiveItemIndex;


    void Start()
    {
        skillData = SkillData.instance;
    }

    // Located on the Skill Item buttons so it knows what information to display.
    // EX: Oak Button under the WoodCuttingSkillMenu contains an On Click Event to this function with the Oak SO
    // Displays all information related to the SO that is active on the button
    public void DisplayActiveMenu(ScriptableObject_SkillingItems activeItem)
    {
        currentItem = activeItem;
        foundActiveItemIndex = FindActiveItem(currentItem);
        resourceName.text = activeItem.nodeName;
        resourceIcon.sprite = activeItem.menuIcon;
        resourceQuanity.text = "Current Amount: " + skillData.inventory.inventoryList[foundActiveItemIndex].stackSize;
        expText.text = "EXP: " + activeItem.exp;
        timeText.text = "Time: " + activeItem.timeTakenToGather + " secs";
        SetMaxProgress();
    }

    // UISTATEMACHINE Active Item Menu Called in the OnUpdate Event Box thing
    public void UpdateDisplayActiveItem()
    {
        if(currentItem == null)
        {
            return;
        }
        resourceName.text = currentItem.nodeName;
        resourceIcon.sprite = currentItem.menuIcon;
        resourceQuanity.text = "Current Amount: " + skillData.inventory.inventoryList[foundActiveItemIndex].stackSize;;
        expText.text = "EXP: " + currentItem.exp;
        timeText.text = "Time: " + currentItem.timeTakenToGather + " secs"; 
        SetProgress();
        
    }

    // Goes through the Inventory, compares the Active Item ID to each inventory Item ID and returns the index so the update function works again
    public int FindActiveItem(ScriptableObject_SkillingItems _activeItem)
    {
        int index = 0;
        for(int i = 0; i < skillData.inventory.inventoryList.Count; i++)
        {
            if(_activeItem.ID == skillData.inventory.inventoryList[i].skillingItem.ID)
            {
                index = i;
            }
        }
        return index;
    }

    public void SetProgress()
    {
        progressSlider.value = currentItem.timeTakenToGather - skillData.timer.timeLeft;
    }
    
    public void SetMaxProgress()
    {
        progressSlider.maxValue = currentItem.timeTakenToGather;
        progressSlider.value = skillData.timer.timeLeft;
    }
}
