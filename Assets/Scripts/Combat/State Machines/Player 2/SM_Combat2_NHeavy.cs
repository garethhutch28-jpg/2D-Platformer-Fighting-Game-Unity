using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SM_Combat2_NHeavy : ICombatSM2
{
    private CombatScript2 combat;
    public Transform NHeavyAttPoint;
    


    public SM_Combat2_NHeavy(CombatScript2 combat)
    {
        this.combat = combat;

    }

    public void Exit()
    {

    }

    public void FixedUpdate()
    {
        nHeavy();
    }

    public void Start()
    {
        Debug.Log("EnteredNLightATT");


    }

    public void Update()
    {

    }

    void nHeavy()

    {

        RaycastHit2D[] raycastHit2Ds = Physics2D.CircleCastAll(combat.NHeavyAttPoint.position, combat.combatData.nHeavyAttRange, Vector2.zero, 0f, combat.enemyLayer);

        foreach (RaycastHit2D hit in raycastHit2Ds)
        {
            hit.collider.GetComponent<P1DamageManager>().TakeDamage(combat.combatData.nHeavyAttackDamage);
            hit.collider.GetComponent<PlatMovement>().NHeavyKnockBack();


        }
        ExtDebug.DrawCircleCast2D(combat.NlightAttPoint.position, combat.combatData.lightAttackRange, Color.green, 1f);
        combat.SetState(new SM_Combat2_Idle(combat));
    }
}
