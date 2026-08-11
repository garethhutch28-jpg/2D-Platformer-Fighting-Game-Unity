using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Neutral Light State
public class SM_Combat_NLight : ICombatSM
{
    private CombatScript combat;//Reference to the main combat script
    public Transform NlightAttPoint;//The transform used as the origin point of the attack

    //Constructor that takes in the combat script reference
    public SM_Combat_NLight(CombatScript combat)
    {
        this.combat = combat;
    }

    //Called when the state is entered
    public void Start()
    {
        Debug.Log("EnteredNLightATT"); //Validatie the state has been entered
        NlightAttPoint = combat.NlightAttPoint; //Initialize the attack point from the combat script
        Debug.Log(NlightAttPoint); //Validat that the attack point is set correctly
    }

    public void Update()
    {

    }

    public void FixedUpdate()
    {
        nLight(); //Execute the neutral light
    }

    public void Exit()
    {

    }

    //neutral light method
    void nLight()
    {
        //throw a circle with an origin, radius, direction, distance and the layer that is affected by if (enemies)
        RaycastHit2D[] raycastHit2Ds = Physics2D.CircleCastAll(NlightAttPoint.position, combat.combatData.lightAttackRange, Vector2.zero, 0f, combat.enemyLayer);

        //for every hit in this circle
        foreach (RaycastHit2D hit in raycastHit2Ds)
        {
            //apply damage and knockback
            hit.collider.GetComponent<P2DamageManager>().TakeDamage(combat.combatData.nLightAttackDamage);
            hit.collider.GetComponent<Plat2Movement>().NLightKnockBack();

        }
        //Draw the circle for the inspector (not important to the logic of the attack just used to help show visual for the game design document
        ExtDebug.DrawCircleCast2D(NlightAttPoint.position, combat.combatData.lightAttackRange, Color.red, 1f);
        combat.SetState(new SM_Combat_Idle(combat));
    }
}


