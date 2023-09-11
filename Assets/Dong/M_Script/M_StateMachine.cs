using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_StateMachine
{
    public M_State currentState { get; private set; }
    
    
    public void Initiate(M_State InitialState)
    {
        currentState = InitialState;
        currentState.Enter();
    }

    public void ChangeState(M_State changingState)
    {
        currentState.Exit();
        currentState = changingState;
        currentState.Enter();
    }
}
