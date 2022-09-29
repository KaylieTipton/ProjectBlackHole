using UnityEngine;

[System.Serializable]
public class SimpleState
{
    private bool startHasBeenCalled = false;

    [Header("State Events")]
    [SerializeField]
    public OnStateStartEvent OnStateStart;
    [SerializeField]
    public OnStateUpdateEvent OnStateUpdate;
    [SerializeField]
    public OnStateExitEvent OnStateExit;

    [HideInInspector]
    public SimpleStateMachine StateMachine;

    public virtual void OnStart()
    {
        OnStateStart.Invoke();
        startHasBeenCalled = true;
    }

    public virtual void UpdateState(float dt)
    {
        if (!startHasBeenCalled)
            return;
        OnStateUpdate.Invoke();
    }

    public virtual void OnExit()
    {
        if (!startHasBeenCalled)
            return;
        OnStateExit.Invoke();
        startHasBeenCalled = false;
    }
}

[System.Serializable]
public class OnStateStartEvent : UnityEngine.Events.UnityEvent { }
[System.Serializable]
public class OnStateUpdateEvent : UnityEngine.Events.UnityEvent { }
[System.Serializable]
public class OnStateExitEvent : UnityEngine.Events.UnityEvent { }

