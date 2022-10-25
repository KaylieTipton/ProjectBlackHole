using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SkillItemMenu : MonoBehaviour
{
    public ScriptableObject_SkillingItems item;
    public TMP_Text skillName;
    public Image resourceIcon;
    public TMP_Text expText;
    public TMP_Text timeText;
    public GameObject lockedImage;




    // Start is called before the first frame update
    void Start()
    {
        DisplaySkill();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Displays information of the item that will be active if the button is selected
    // EX: The Oak Button displays all information relating to the Oak Tree Item
    public void DisplaySkill()
    {
        skillName.text = item.nodeName;
        resourceIcon.sprite = item.menuIcon;
        expText.text = "Exp Gained: " + item.exp;
        timeText.text = "Time:  " + item.timeTakenToGather + " sec";
    
    }
}
