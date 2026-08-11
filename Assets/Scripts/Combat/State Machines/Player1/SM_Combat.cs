using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Main Combat State Machine class
public class SM_Combat 
{
    //Current active state reference
    private ICombatSM currentState;

    //Change State Function
    public void ChangeState(ICombatSM newState)
    {
        //Call the exit method on the current state
        currentState?.Exit();
        //Set the current state to the new state
        currentState = newState;
        //Call the start method on the new current state
        currentState?.Start();
    }
    //Get state method
    public ICombatSM GetState()
    {
        return currentState;
    }
    //Update logic for the state machine
    public void Update()
    {
        currentState?.Update();
    }
    //Fixed update logic for the state machine
    public void FixedUpdate()
    {
        currentState?.FixedUpdate();    
    }
}








