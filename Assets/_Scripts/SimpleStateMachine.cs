using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SimpleStateMachine : MonoBehaviour
{
    [Header("States")]
    [HideInInspector]
    public List<SimpleState> States;
    public string StateName;
    protected SimpleState state = null;

    private void SetState(SimpleState s)
    {
        if (s == null)
            return;
        //Checking current state. If its not null call onexit. IF null dont call on exit
        if (state != null)
            state.OnExit();//different state from below state.

        state = s;

        state.OnStart();
    }

    public void ChangeState(string stateName)
    {
        foreach (SimpleState _state in States)
        {
            if (stateName.ToLower() == _state.GetType().ToString().ToLower())
            {
                SetState(_state);
                Debug.Log("State Changed: " + nameof(_state));
                StateName = stateName;
                return;
            }
        }

    //List<SimpleState> st = States.Where(s => s.GetType().ToString().ToLower() == stateName.ToLower()).ToList<SimpleState>();
    }
    private void FixedUpdate()
    {
        if (state == null)
            return;
        state.UpdateState(Time.deltaTime);
    }
}
