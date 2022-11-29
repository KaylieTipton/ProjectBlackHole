using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// Should probably fix this since SkillData is now single Town *Glares at suspiciously*

public class SkillMenu : MonoBehaviour
{
    public TMP_Text skillLevel;
    public TMP_Text skillExp;
    public SkillData skillInfo;
    public CURRENTSKILL CURRENTSKILL;

    // Display Skill Info based on what skill it is
    public void DisplaySkillInfo()
    {
        switch(CURRENTSKILL)
        {
            case CURRENTSKILL.WOODCUTTING:
                DisplayWoodCutInfo();
                break;
        }
    }


    // Does the Displaying of the Woodcut Info
    public void DisplayWoodCutInfo()
    {
        skillLevel.text = "Level: " + skillInfo.woodcutLevel;
        skillExp.text = "Experiance: " + skillInfo.woodcutExp + " / " + skillInfo.woodcutLevel * 100;
    }
}
