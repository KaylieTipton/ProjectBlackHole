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
    public UIActiveItemState uIActiveItemState;

    // Start is called before the first frame update
    void Start()
    {
        uIMainGameMenu.uIStateMachine = this;
        States.Add(uIMainGameMenu);
        uISkillsState.uIStateMachine = this;
        States.Add(uISkillsState);
        uICharacterState.uIStateMachine = this;
        States.Add(uICharacterState);
        uIWoodcuttingState.uIStateMachine = this;
        States.Add(uIWoodcuttingState);
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
    }

//Woodcutting Resource Buttons
    public void ActiveItemButton()
    {
        ChangeState(nameof(uIActiveItemState));
    }

}
