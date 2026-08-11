using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatScript : MonoBehaviour
{
    private SM_Combat stateMachine;

    //Transforms for all hitboxes
    public Transform NlightAttPoint;
    public Transform SLightAttPoint;
    public Transform DLightAttPoint;
    public Transform NAirAttPoint;
    public Transform SAirAttPoint;
    public Transform DAirAttPoint;
    public Transform NHeavyAttPoint;
    public Transform SHeavyAttPoint;

    //What is an enemy
    public LayerMask enemyLayer;

    //References to other classes
    public CombatData combatData;
    public P2DamageManager p2Damage;
    public PlatMovement movement;
    public Plat2Movement movement2;
    public P1CharacterSelected characterP1;
    public P2CharacterSelected characterP2;

    //Animations
    public Animator anim;
    public bool isSideAttacking = false;
    public bool isNeutralAttacking = false;
    public bool isDownAttacking = false;
    public bool isSideHeavyAttacking = false;
    public bool isNeutralHeavyAttacking = false;

    private void Start()
    {
        //Load the statemachine
        stateMachine = new SM_Combat();
        //Set the initial state to idle
        stateMachine.ChangeState(new SM_Combat_Idle(this));
    }
    private void Update()
    {
        //Make sure the state machine updates are running in the update function (Run once per frame)
        stateMachine.Update();
       
    }

    private void FixedUpdate()
    {
        //Make sure the state machine fixed update functions are running in the fixed update function
        stateMachine.FixedUpdate();
    }
    public void SetState(ICombatSM state)
    {
        //Change the state of the state machine to the provided one (when changestate is called)
        stateMachine.ChangeState (state);
    }

}
