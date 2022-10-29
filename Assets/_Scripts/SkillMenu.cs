using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SkillMenu : MonoBehaviour
{
    public TMP_Text skillLevel;
    public TMP_Text skillExp;
    public SkillData skillInfo;
    public CurrentSkill currentSkill;

    public void DisplaySkillInfo()
    {
        switch(currentSkill)
        {
            case CurrentSkill.Woodcutting:
                DisplayWoodCutInfo();
                break;
        }
    }


    public void DisplayWoodCutInfo()
    {
        skillLevel.text = "Level: " + skillInfo.woodcutLevel;
        skillExp.text = "Experiance: " + skillInfo.woodcutExp + " / " + skillInfo.woodcutLevel * 100;
    }
}
