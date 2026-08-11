using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SM_Combat2_DLight : ICombatSM2
{
    private CombatScript2 combat;
    public Transform DlightAttPoint;
    public SM_Combat2_DLight(CombatScript2 combat)
    {
        this.combat = combat;

    }
    public void Exit()
    {

    }

    public void FixedUpdate()
    {
        dLight();
    }

    public void Start()
    {
        Debug.Log("EnteredDLightATT");
        DlightAttPoint = combat.DLightAttPoint;
        Debug.Log(DlightAttPoint);
    }

    public void Update()
    {

    }

    void dLight()
    {

        RaycastHit2D[] raycastHit2Ds = Physics2D.BoxCastAll(DlightAttPoint.position, new Vector2(2f, 2f), 0f, Vector2.zero, 0f, combat.enemyLayer);

        foreach (RaycastHit2D hit in raycastHit2Ds)
        {
            hit.collider.GetComponent<P1DamageManager>().TakeDamage(combat.combatData.dLightAttackDamage);
            hit.collider.GetComponent<PlatMovement>().DLightKnockBack();
        }
        ExtDebug.DrawBoxCast2D(DlightAttPoint.position, new Vector2(2f, 0.4f), 0f, Vector2.zero, 0f, Color.red, 1f);
        combat.SetState(new SM_Combat2_Idle(combat));
    }
}
