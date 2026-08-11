using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SM_Combat2_SLight : ICombatSM2
{
    private CombatScript2 combat;
    public Transform SlightAttPoint;

    public SM_Combat2_SLight(CombatScript2 combat)
    {
        this.combat = combat;

    }

    public void Start()
    {
        Debug.Log("EnteredSLightATT");
        SlightAttPoint = combat.SLightAttPoint;
        Debug.Log(SlightAttPoint);
    }

    public void Update()
    {

    }

    public void FixedUpdate()
    {
        sLight();
    }

    public void Exit()
    {

    }

    void sLight()
    {

        RaycastHit2D[] raycastHit2Ds = Physics2D.BoxCastAll(SlightAttPoint.position, new Vector2(2f, 2f), 0f, Vector2.zero, 0f, combat.enemyLayer);

        foreach (RaycastHit2D hit in raycastHit2Ds)
        {
            hit.collider.GetComponent<P1DamageManager>().TakeDamage(combat.combatData.sLightAttackDamage);
            hit.collider.GetComponent<PlatMovement>().SLightKnockBack();
        }
        ExtDebug.DrawBoxCast2D(SlightAttPoint.position, new Vector2(2f, 0.8f), 0f, Vector2.zero, 0f, Color.red, 1f);
        combat.SetState(new SM_Combat2_Idle(combat));
    }
}
