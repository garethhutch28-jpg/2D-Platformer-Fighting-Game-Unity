using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Down Air State
public class SM_Combat_DAir : ICombatSM
{
    private CombatScript combat;//Reference to the main combat script
    public Transform DAirAttPoint;//The transform used as the origin point of the attack

    //Constructor that takes in the combat script reference
    public SM_Combat_DAir(CombatScript combat)
    {
        this.combat = combat;
    }

    //Called when the state is entered
    public void Start()
    {
        DAirAttPoint = combat.DAirAttPoint; //Initialize the attack point from the combat script
    }

    public void Update()
    {

    }

    public void FixedUpdate()
    {
        dAir(); //Execute the down air
    }

    public void Exit()
    {

    }

    //down air method
    void dAir()
    {
        //throw a box with an origin, size, angle, direction, distance and the layer that is affected by it (enemies)
        RaycastHit2D[] raycastHit2Ds = Physics2D.BoxCastAll(DAirAttPoint.position, new Vector2(2f, 2f), 0f, Vector2.zero, 0f, combat.enemyLayer);

        //for every hit in this box
        foreach (RaycastHit2D hit in raycastHit2Ds)
        {
            //apply damage and knockback
            hit.collider.GetComponent<P2DamageManager>().TakeDamage(combat.combatData.dAirAttackDamage);
            hit.collider.GetComponent<Plat2Movement>().DAirKnockBack();
        }
        //Draw the box for the inspector (not important to the logic of the attack just used to help show visual for the game design document)
        ExtDebug.DrawBoxCast2D(DAirAttPoint.position, new Vector2(0.8f, 2f), 0f, Vector2.zero, 0f, Color.blue, 1f);
        combat.SetState(new SM_Combat_Idle(combat));
    }
}

