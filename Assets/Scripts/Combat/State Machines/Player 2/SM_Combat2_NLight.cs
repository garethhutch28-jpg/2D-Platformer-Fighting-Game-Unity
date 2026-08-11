using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SM_Combat2_NLight : ICombatSM2
{
    private CombatScript2 combat;
    public Transform NlightAttPoint;
   


    public SM_Combat2_NLight(CombatScript2 combat)
    {
        this.combat = combat;

    }

    public void Exit()
    {

    }

    public void FixedUpdate()
    {
        nLight();
    }

    public void Start()
    {
        Debug.Log("EnteredNLightATT");


    }

    public void Update()
    {

    }

    void nLight()

    {

        RaycastHit2D[] raycastHit2Ds = Physics2D.CircleCastAll(combat.NlightAttPoint.position, combat.combatData.lightAttackRange, Vector2.zero, 0f, combat.enemyLayer);

        foreach (RaycastHit2D hit in raycastHit2Ds)
        {
            hit.collider.GetComponent<P1DamageManager>().TakeDamage(combat.combatData.nLightAttackDamage);
            hit.collider.GetComponent<PlatMovement>().NLightKnockBack();


        }
        ExtDebug.DrawCircleCast2D(combat.NlightAttPoint.position, combat.combatData.lightAttackRange, Color.red, 1f);
        combat.SetState(new SM_Combat2_Idle(combat));
    }
}
