using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ExecutionState
{
    NONE,
    ACTIVATE,
    COMPLETED,
    TERMINATED,
};
public abstract class abstractFSMState : ScriptableObject
{
    public ExecutionState ExecutionState {get; protected set;}
    public virtual void OnEnable()
    {
        ExecutionState = ExecutionState.NONE;
    }
    public virtual bool EnterState()
    {
        ExecutionState = ExecutionState.ACTIVATE;
        return true;
    }

    public abstract void UpdateState();

    public virtual bool ExitState()
    {
        ExecutionState = ExecutionState.COMPLETED;
        return true;
    }


}
