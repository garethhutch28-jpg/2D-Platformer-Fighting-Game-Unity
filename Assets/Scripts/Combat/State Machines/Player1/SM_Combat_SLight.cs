using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Side Light State
public class SM_Combat_SLight : ICombatSM
{
    private CombatScript combat;//Reference to the main combat script
    public Transform SlightAttPoint;//The transform used as the origin point of the attack

    //Constructor that takes in the combat script reference
    public SM_Combat_SLight(CombatScript combat)
    {
        this.combat = combat;
    }

    //Called when the state is entered
    public void Start()
    {
        Debug.Log("EnteredSLightATT"); //Validatie the state has been entered
        SlightAttPoint = combat.SLightAttPoint; //Initialize the attack point from the combat script
        Debug.Log(SlightAttPoint); //Validate that the attack point is set correctly
    }

   
    public void Update()
    {
        
    }

 
    public void FixedUpdate()
    {
        sLight(); //Execute the side light
    }


    public void Exit()
    {
  
    }

    //side light method
    void sLight()
    {
        //throw a box with an origin, size, angle, direction, sitance and the layer that is affected by if (enemies)
        RaycastHit2D[] raycastHit2Ds = Physics2D.BoxCastAll(SlightAttPoint.position, new Vector2(2f, 2f), 0f, Vector2.zero, 0f, combat.enemyLayer);

        //for every hit in this box
        foreach (RaycastHit2D hit in raycastHit2Ds)
        {
            //apply damage and knockback
            hit.collider.GetComponent<P2DamageManager>().TakeDamage(combat.combatData.sLightAttackDamage);
            hit.collider.GetComponent<Plat2Movement>().SLightKnockBack(); 

        }
        //Draw the box for the inspector (not important to the logic of the attack just used to help show visual for the game design document
        ExtDebug.DrawBoxCast2D(SlightAttPoint.position, new Vector2(2f, 0.8f), 0f, Vector2.zero, 0f, Color.red, 1f);
        combat.SetState(new SM_Combat_Idle(combat));
    }
}


