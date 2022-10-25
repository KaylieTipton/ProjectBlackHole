using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ActiveItemMenu : MonoBehaviour
{
    public TMP_Text resourceName; 
    public Image resourceIcon;
    public TMP_Text resourceQuanity;
    public TMP_Text expText;
    public TMP_Text timeText;

    public void DisplayActiveMenu(ScriptableObject_SkillingItems activeItem)
    {
        resourceName.text = activeItem.itemName;
        resourceIcon.sprite = activeItem.menuIcon;
        resourceQuanity.text = "Current Amount: " + activeItem.quanity;
        expText.text = "EXP: " + activeItem.exp;
        timeText.text = "Time: " + activeItem.timeTakenToGather + " secs";        
    }
}
