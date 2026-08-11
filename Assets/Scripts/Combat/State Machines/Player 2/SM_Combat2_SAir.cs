using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SM_Combat2_SAir : ICombatSM2
{
    private CombatScript2 combat;
    public Transform SAirAttPoint;
    public SM_Combat2_SAir(CombatScript2 combat)
    {
        this.combat = combat;

    }
    public void Start()
    {
        SAirAttPoint = combat.SAirAttPoint;
    }

    public void Update()
    {

    }

    public void FixedUpdate()
    {
        sAir();
    }

    public void Exit()
    {

    }
    void sAir()
    {

        RaycastHit2D[] raycastHit2Ds = Physics2D.BoxCastAll(SAirAttPoint.position, new Vector2(2f, 2f), 0f, Vector2.zero, 0f, combat.enemyLayer);

        foreach (RaycastHit2D hit in raycastHit2Ds)
        {
            hit.collider.GetComponent<P1DamageManager>().TakeDamage(combat.combatData.sAirAttackDamage);
            hit.collider.GetComponent<PlatMovement>().SAirKnockBack();
        }
        ExtDebug.DrawBoxCast2D(SAirAttPoint.position, new Vector2(2f, 0.8f), 0f, Vector2.zero, 0f, Color.blue, 1f);
        combat.SetState(new SM_Combat2_Idle(combat));
    }
}
