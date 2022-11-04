using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

// Contains everything relating to the active item menu.
// Displays the active item information and updates the information
public class ActiveItemMenu : MonoBehaviour
{
    public TMP_Text resourceName; 
    public Image resourceIcon;
    public TMP_Text resourceQuanity;
    public TMP_Text expText;
    public TMP_Text timeText;

    public SkillData skillData;
    public Slider progressSlider;


    private ScriptableObject_SkillingItems currentItem;

    // Located on the Skill Item buttons so it knows what information to display.
    // EX: Oak Button under the WoodCuttingSkillMenu contains an On Click Event to this function with the Oak SO
    // Displays all information related to the SO that is active on the button
    public void DisplayActiveMenu(ScriptableObject_SkillingItems activeItem)
    {
        currentItem = activeItem;
        resourceName.text = activeItem.nodeName;
        resourceIcon.sprite = activeItem.menuIcon;
        resourceQuanity.text = "Current Amount: " + activeItem.quanity;
        expText.text = "EXP: " + activeItem.exp;
        timeText.text = "Time: " + activeItem.timeTakenToGather + " secs";
        SetMaxProgress();
    }

    public void UpdateDisplayActiveItem()
    {
        if(currentItem == null)
        {
            return;
        }
        resourceName.text = currentItem.nodeName;
        resourceIcon.sprite = currentItem.menuIcon;
        resourceQuanity.text = "Current Amount: " + currentItem.quanity;
        expText.text = "EXP: " + currentItem.exp;
        timeText.text = "Time: " + currentItem.timeTakenToGather + " secs"; 
        SetProgress();
        
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
