using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatScript2 : MonoBehaviour
{
    private SM_Combat2 stateMachine;

    public Transform NlightAttPoint;
    public Transform SLightAttPoint;
    public Transform DLightAttPoint;
    public Transform NAirAttPoint;
    public Transform SAirAttPoint;
    public Transform DAirAttPoint;
    public Transform NHeavyAttPoint;
    public Transform SHeavyAttPoint;

    public LayerMask enemyLayer;
    public CombatData combatData;
    
    public P1DamageManager p1Damage;
    public PlatMovement movement;
    public Plat2Movement movement2;
    private void Start()
    {
        stateMachine = new SM_Combat2();
        stateMachine.ChangeState(new SM_Combat2_Idle(this));
    }
    private void Update()
    {
        stateMachine.Update();
    }

    private void FixedUpdate()
    {
        stateMachine.FixedUpdate();
    }
    public void SetState(ICombatSM2 state)
    {
        stateMachine.ChangeState(state);
    }
}
