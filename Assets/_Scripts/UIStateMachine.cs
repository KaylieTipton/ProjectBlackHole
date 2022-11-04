using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIStateMachine : SimpleStateMachine
{
    public UIMainGameMenu uIMainGameMenu;
    public UISkillsState uISkillsState;
    public UIInventoryState uIInventoryState;
    public UICharacterState uICharacterState;
    public UIWoodcuttingState uIWoodcuttingState;
    public UIMiningState uIMiningState;
    public UIActiveItemState uIActiveItemState;

    public CurrentSkill currentSkill;

    // Start is called before the first frame update
    void Start()
    {
        uIMainGameMenu.uIStateMachine = this;
        States.Add(uIMainGameMenu);
        uISkillsState.uIStateMachine = this;
        States.Add(uISkillsState);
        uIInventoryState.uIStateMachine = this;
        States.Add(uIInventoryState);
        uICharacterState.uIStateMachine = this;
        States.Add(uICharacterState);
        uIWoodcuttingState.uIStateMachine = this;
        States.Add(uIWoodcuttingState);
        uIMiningState.uIStateMachine = this;
        States.Add(uIMiningState);
        uIActiveItemState.uIStateMachine = this;
        States.Add(uIActiveItemState);



        
        foreach (SimpleState state in States)
        {
            state.StateMachine = this;
        }

        ChangeState(nameof(uIMainGameMenu));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

//Back buton for Active Menu
    public void ActiveBackButton()
    {
        switch(currentSkill)
        {
            case CurrentSkill.Woodcutting:
                ChangeState(nameof(uIWoodcuttingState));
                break;
            case CurrentSkill.Mining:
                ChangeState(nameof(uIMiningState));
                break;
        }
    }
//Main Game Menu Buttons
    public void SkillsButton()
    {
        ChangeState(nameof(uISkillsState));
    }

    public void InventoryButton()
    {
        ChangeState(nameof(uIInventoryState));
    }

    public void CharacterButton()
    {
        ChangeState(nameof(uICharacterState));
    }

//Skills Buttons
    public void WoodcuttingButton()
    {
        ChangeState(nameof(uIWoodcuttingState));
        currentSkill = CurrentSkill.Woodcutting;
    }

    public void MiningButton()
    {
        ChangeState(nameof(uIMiningState));
        currentSkill = CurrentSkill.Mining;
    }

//Woodcutting Resource Buttons
    public void ActiveItemButton()
    {
        ChangeState(nameof(uIActiveItemState));
    }



}
