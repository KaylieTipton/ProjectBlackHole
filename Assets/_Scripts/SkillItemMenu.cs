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

    public void DisplaySkill()
    {
        skillName.text = item.itemName;
        resourceIcon.sprite = item.menuIcon;
        expText.text = "Exp Gained: " + item.exp;
        timeText.text = "Time:  " + item.timeTakenToGather + " sec";
    
    }
}
