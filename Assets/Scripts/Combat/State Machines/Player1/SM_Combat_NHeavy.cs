using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Neutral / Down Heavy State
public class SM_Combat_NHeavy : ICombatSM
{
    private CombatScript combat;//Reference to the main combat script
    public Transform NHeavyAttPoint;//The transform used as the origin point of the attack
    private P2DamageManager P2DamageManager;

    //Constructor that takes in the combat script reference
    public SM_Combat_NHeavy(CombatScript combat)
    {
        this.combat = combat;
    }

    //Called when the state is entered
    public void Start()
    {
        Debug.Log("EnteredNHeavyATT"); //Validate the state has been entered
    }

    public void Update()
    {

    }

    public void FixedUpdate()
    {
        nHeavy(); //Execute the neutral heavy
    }

    public void Exit()
    {

    }

    //neutral heavy method
    void nHeavy()
    {
        //throw a circle with an origin, size, angle, direction, distance, and the layer that is affected by it (enemies)
        RaycastHit2D[] raycastHit2Ds = Physics2D.CircleCastAll(combat.NHeavyAttPoint.position, combat.combatData.nHeavyAttRange, Vector2.zero, 0f, combat.enemyLayer);

        //for every hit in this circle
        foreach (RaycastHit2D hit in raycastHit2Ds)
        {
            //apply damage and knockback
            hit.collider.GetComponent<P2DamageManager>().TakeDamage(combat.combatData.nHeavyAttackDamage);
            hit.collider.GetComponent<Plat2Movement>().NHeavyKnockBack();
        }
        //Draw the circle for the inspector (not important to the logic of the attack just used to help show visual for the game design document)
        ExtDebug.DrawCircleCast2D(combat.NlightAttPoint.position, combat.combatData.lightAttackRange, Color.green, 1f);
        combat.SetState(new SM_Combat_Idle(combat));
    }
}

