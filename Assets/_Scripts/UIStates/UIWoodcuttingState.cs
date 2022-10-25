using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class UIWoodcuttingState : UIState
{
    public List<ButtonSkillingItemPairs> buttonList;
    public override void OnStart()
    {
        base.OnStart();
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

//DO FOR ALL SKILL STATES
    public void CalculateUnlock() {
        foreach(ButtonSkillingItemPairs bsPairs in buttonList)
        {
            bsPairs.button.interactable = bsPairs.skillingItem.isUnlocked;
            bsPairs.button.GetComponent<SkillItemMenu>().lockedImage.SetActive(!bsPairs.skillingItem.isUnlocked);
        }
    }
}


[System.Serializable]
public class ButtonSkillingItemPairs
{
    public Button button;
    public ScriptableObject_SkillingItems skillingItem;

}
