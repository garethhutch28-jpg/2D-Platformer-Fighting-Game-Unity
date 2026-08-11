using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SM_Combat2_DAir : ICombatSM2
{
    private CombatScript2 combat;
    public Transform DAirAttPoint;

    public SM_Combat2_DAir(CombatScript2 combat)
    {
        this.combat = combat;

    }
    public void Exit()
    {

    }

    public void FixedUpdate()
    {
        dAir();
    }

    public void Start()
    {
        DAirAttPoint = combat.DAirAttPoint;
    }

    public void Update()
    {

    }

    void dAir()
    {

        RaycastHit2D[] raycastHit2Ds = Physics2D.BoxCastAll(DAirAttPoint.position, new Vector2(2f, 2f), 0f, Vector2.zero, 0f, combat.enemyLayer);

        foreach (RaycastHit2D hit in raycastHit2Ds)
        {
            hit.collider.GetComponent<P1DamageManager>().TakeDamage(combat.combatData.dAirAttackDamage);
            hit.collider.GetComponent<PlatMovement>().DAirKnockBack();

        }
        ExtDebug.DrawBoxCast2D(DAirAttPoint.position, new Vector2(0.8f, 2f), 0f, Vector2.zero, 0f, Color.blue, 1f);
        combat.SetState(new SM_Combat2_Idle(combat));
    }
}
