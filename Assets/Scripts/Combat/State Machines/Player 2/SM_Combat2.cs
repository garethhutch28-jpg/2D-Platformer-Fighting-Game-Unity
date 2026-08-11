using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SM_Combat2
{
    private ICombatSM2 currentState;
    public void ChangeState(ICombatSM2 newState)
    {
        currentState?.Exit();
        currentState = newState;
        currentState?.Start();
    }
    public ICombatSM2 GetState()
    {
        return currentState;
    }
    public void Update()
    {
        currentState?.Update();
    }
    public void FixedUpdate()
    {
        currentState?.FixedUpdate();
    }
}
