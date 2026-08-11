using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SM_Combat2_NAir : ICombatSM2
{
    private CombatScript2 combat;
    public Transform NAirAttPoint;

    //constructor
    public SM_Combat2_NAir(CombatScript2 combat)
    {
        this.combat = combat;

    }

    public void Exit()
    {

    }

    public void FixedUpdate()
    {
        nAir();
    }

    public void Start()
    {
        NAirAttPoint = combat.NAirAttPoint;
    }

    public void Update()
    {

    }

    void nAir()
    {

        RaycastHit2D[] raycastHit2Ds = Physics2D.CircleCastAll(NAirAttPoint.position, combat.combatData.lightAttackRange, Vector2.zero, 0f, combat.enemyLayer);

        foreach (RaycastHit2D hit in raycastHit2Ds)
        {
            hit.collider.GetComponent<P1DamageManager>().TakeDamage(combat.combatData.nAirAttackDamage);
            hit.collider.GetComponent<PlatMovement>().NAirKnockBack();
        }
        ExtDebug.DrawCircleCast2D(NAirAttPoint.position, combat.combatData.airAttackRange, Color.blue, 1f);
        combat.SetState(new SM_Combat2_Idle(combat));
    }
}
    

