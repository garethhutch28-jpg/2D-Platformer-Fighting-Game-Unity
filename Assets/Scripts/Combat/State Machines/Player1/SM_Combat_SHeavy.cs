using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

//Side Heavy State
public class SM_Combat_SHeavy : ICombatSM
{
    private CombatScript combat;//Reference to the main combat script
    public Transform SHeavyAttPoint;//The transform used as the origin point of the attack

    //Constructor that takes in the combat script reference
    public SM_Combat_SHeavy(CombatScript combat)
    {
        this.combat = combat;
    }

    //Called when the state is entered
    public void Start()
    {
        SHeavyAttPoint = combat.SHeavyAttPoint; //Initialize the attack point from the combat script
    }

    public void Update()
    {

    }

    public void FixedUpdate()
    {
        sHeavy(); //Execute the side heavy
    }

    public void Exit()
    {

    }

    //side heavy method
    void sHeavy()
    {
        //throw a box with an origin, size, angle, direction, distance and the layer that is affected by it (enemies)
        RaycastHit2D[] raycastHit2Ds = Physics2D.BoxCastAll(SHeavyAttPoint.position, new Vector2(2f, 1.5f), 0f, Vector2.zero, 0f, combat.enemyLayer);

        //for every hit in this box
        foreach (RaycastHit2D hit in raycastHit2Ds)
        {
            //apply damage and knockback
            hit.collider.GetComponent<P2DamageManager>().TakeDamage(combat.combatData.sHeavyAttackDamage);
            hit.collider.GetComponent<Plat2Movement>().SHeavyKnockBack();
        }
        //Draw the box for the inspector (not important to the logic of the attack just used to help show visual for the game design document)
        ExtDebug.DrawBoxCast2D(SHeavyAttPoint.position, new Vector2(2f, 0.8f), 0f, Vector2.zero, 0f, Color.green, 1f);
        combat.SetState(new SM_Combat_Idle(combat));
    }
}

