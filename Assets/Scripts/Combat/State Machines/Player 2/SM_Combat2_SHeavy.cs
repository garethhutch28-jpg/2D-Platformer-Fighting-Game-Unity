using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SM_Combat2_SHeavy : ICombatSM2
{
    private CombatScript2 combat;
    public Transform SHeavyAttPoint;

    //constructor
    public SM_Combat2_SHeavy(CombatScript2 combat)
    {
        this.combat = combat;

    }

    public void Exit()
    {

    }

    public void FixedUpdate()
    {
        sHeavy();
    }

    public void Start()
    {
        SHeavyAttPoint = combat.SHeavyAttPoint;
    }

    public void Update()
    {

    }

    void sHeavy()
    {

        RaycastHit2D[] raycastHit2Ds = Physics2D.BoxCastAll(SHeavyAttPoint.position, new Vector2(2f, 1.5f), 0f, Vector2.zero, 0f, combat.enemyLayer);

        foreach (RaycastHit2D hit in raycastHit2Ds)
        {
            hit.collider.GetComponent<P1DamageManager>().TakeDamage(combat.combatData.sHeavyAttackDamage);
            hit.collider.GetComponent<PlatMovement>().SHeavyKnockBack();
        }
        ExtDebug.DrawBoxCast2D(SHeavyAttPoint.position, new Vector2(2f, 0.8f), 0f, Vector2.zero, 0f, Color.green, 1f);
        combat.SetState(new SM_Combat2_Idle(combat));
    }
}
