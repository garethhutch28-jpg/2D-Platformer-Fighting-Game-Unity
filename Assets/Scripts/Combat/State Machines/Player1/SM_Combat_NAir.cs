using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Neutral Air State
public class SM_Combat_NAir : ICombatSM
{
    private CombatScript combat;//Reference to the main combat script
    public Transform NAirAttPoint;//The transform used as the origin point of the attack

    //Constructor that takes in the combat script reference
    public SM_Combat_NAir(CombatScript combat)
    {
        this.combat = combat;
    }

    //Called when the state is entered
    public void Start()
    {
        NAirAttPoint = combat.NAirAttPoint; //Initialize the attack point from the combat script
    }

    public void Update()
    {

    }

    public void FixedUpdate()
    {
        nAir(); //Execute the neutral air
    }

    public void Exit()
    {

    }

    //neutral air method
    void nAir()
    {
        //throw a circle with an origin, radius, direction, distance and the layer that is affected by it (enemies)
        RaycastHit2D[] raycastHit2Ds = Physics2D.CircleCastAll(NAirAttPoint.position, combat.combatData.lightAttackRange, Vector2.zero, 0f, combat.enemyLayer);

        //for every hit in this circle
        foreach (RaycastHit2D hit in raycastHit2Ds)
        {
            //apply damage and knockback
            hit.collider.GetComponent<P2DamageManager>().TakeDamage(combat.combatData.nAirAttackDamage);
            hit.collider.GetComponent<Plat2Movement>().NAirKnockBack();
        }
        //Draw the circle for the inspector (not important to the logic of the attack just used to help show visual for the game design document)
        ExtDebug.DrawCircleCast2D(NAirAttPoint.position, combat.combatData.airAttackRange, Color.blue, 1f);
        combat.SetState(new SM_Combat_Idle(combat));
    }
}

