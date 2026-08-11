using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Down Light State
public class SM_Combat_DLight : ICombatSM
{
    private CombatScript combat;//Reference to the main combat script
    public Transform DlightAttPoint;//The transform used as the origin point of the attack

    //Constructor that takes in the combat script reference
    public SM_Combat_DLight(CombatScript combat)
    {
        this.combat = combat;
    }

    //Called when the state is entered
    public void Start()
    {
        Debug.Log("EnteredDLightATT"); //Validate the state has been entered
        DlightAttPoint = combat.DLightAttPoint; //Initialize the attack point from the combat script
        Debug.Log(DlightAttPoint); //Validate that the attack point is set correctly
    }

    public void Update()
    {

    }

    public void FixedUpdate()
    {
        dLight(); //Execute the down light
    }

    public void Exit()
    {

    }

    //down light method
    void dLight()
    {
        //throw a box with an origin, size, angle, direction, distance and the layer that is affected by it (enemies)
        RaycastHit2D[] raycastHit2Ds = Physics2D.BoxCastAll(DlightAttPoint.position, new Vector2(2f, 2f), 0f, Vector2.zero, 0f, combat.enemyLayer);

        //for every hit in this box
        foreach (RaycastHit2D hit in raycastHit2Ds)
        {
            //apply damage and knockback
            hit.collider.GetComponent<P2DamageManager>().TakeDamage(combat.combatData.dLightAttackDamage);
            hit.collider.GetComponent<Plat2Movement>().DLightKnockBack();
        }
        //Draw the box for the inspector (not important to the logic of the attack just used to help show visual for the game design document)
        ExtDebug.DrawBoxCast2D(DlightAttPoint.position, new Vector2(2f, 0.4f), 0f, Vector2.zero, 0f, Color.red, 1f);
        combat.SetState(new SM_Combat_Idle(combat));
    }
}
