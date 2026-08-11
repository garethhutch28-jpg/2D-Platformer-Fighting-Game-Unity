using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Side Air State
public class SM_Combat_SAir : ICombatSM
{
    private CombatScript combat;//Reference to the main combat script
    public Transform SAirAttPoint;//The transform used as the origin point of the attack

    //Constructor that takes in the combat script reference
    public SM_Combat_SAir(CombatScript combat)
    {
        this.combat = combat;
    }

    //Called when the state is entered
    public void Start()
    {
        SAirAttPoint = combat.SAirAttPoint; //Initialize the attack point from the combat script
    }

    public void Update()
    {

    }

    public void FixedUpdate()
    {
        sAir(); //Execute the side air
    }

    public void Exit()
    {

    }

    //side air method
    void sAir()
    {
        //throw a box with an origin, size, angle, direction, distance and the layer that is affected by it (enemies)
        RaycastHit2D[] raycastHit2Ds = Physics2D.BoxCastAll(SAirAttPoint.position, new Vector2(2f, 2f), 0f, Vector2.zero, 0f, combat.enemyLayer);

        //for every hit in this box
        foreach (RaycastHit2D hit in raycastHit2Ds)
        {
            //apply damage and knockback
            hit.collider.GetComponent<P2DamageManager>().TakeDamage(combat.combatData.sAirAttackDamage);
            hit.collider.GetComponent<Plat2Movement>().SAirKnockBack();
        }
        //Draw the box for the inspector (not important to the logic of the attack just used to help show visual for the game design document)
        ExtDebug.DrawBoxCast2D(SAirAttPoint.position, new Vector2(2f, 0.8f), 0f, Vector2.zero, 0f, Color.blue, 1f);
        combat.SetState(new SM_Combat_Idle(combat));
    }
}

