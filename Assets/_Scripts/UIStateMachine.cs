using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIStateMachine : SimpleStateMachine
{
    // All of the UI States Help
    public UIMainGameMenu uIMainGameMenu;
    public UISkillsState uISkillsState;
    public UIInventoryState uIInventoryState;
    public UICharacterState uICharacterState;
    public UIWoodcuttingState uIWoodcuttingState;
    public UIMiningState uIMiningState;

    public UIBuildingState uIBuildingState;
    public UIActiveItemState uIActiveItemState;
    public UITownState uITownState;

    public CURRENTSKILL CURRENTSKILL;

    // Start is called before the first frame update
    void Start()
    {
        // Hand Wavey Magic
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

        uIBuildingState.uIStateMachine = this;
        States.Add(uIBuildingState);
        uITownState.uIStateMachine = this;
        States.Add(uITownState);
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
        switch (CURRENTSKILL)
        {
            case CURRENTSKILL.WOODCUTTING:
                ChangeState(nameof(uIWoodcuttingState));
                break;
            case CURRENTSKILL.MINING:
                ChangeState(nameof(uIMiningState));
                break;
            case CURRENTSKILL.BUILDING:
                ChangeState(nameof(uIBuildingState));
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

    public void TownButton()
    {
        ChangeState(nameof(uITownState));
    }

    //Skills Buttons
    public void WoodcuttingButton()
    {
        ChangeState(nameof(uIWoodcuttingState));
        CURRENTSKILL = CURRENTSKILL.WOODCUTTING;
    }

    public void MiningButton()
    {
        ChangeState(nameof(uIMiningState));
        CURRENTSKILL = CURRENTSKILL.MINING;
    }

    public void BuildingButton()
    {
        ChangeState(nameof(uIBuildingState));
        CURRENTSKILL = CURRENTSKILL.BUILDING;
    }

    //Misc Buttons
    public void ActiveItemButton()
    {
        ChangeState(nameof(uIActiveItemState));
    }





}
