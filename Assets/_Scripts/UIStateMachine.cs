using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIStateMachine : SimpleStateMachine
{
    public UISkillsState uISkillsState;
    public UIInventoryState uIInventoryState;
    public UIMainGameMenu uIMainGameMenu;

    // Start is called before the first frame update
    void Start()
    {
        uIMainGameMenu.uIStateMachine = this;
        States.Add(uIMainGameMenu);
        uISkillsState.uIStateMachine = this;
        States.Add(uISkillsState);
        
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

    public void SkillsButton()
    {
        ChangeState(nameof(uISkillsState));
    }
}
