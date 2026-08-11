using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SM_Combat_Idle : ICombatSM
{
    //reference to combat script
    private CombatScript combat;
    //Light and heavy attack cooldown timers
    private float lTimer;
    private float hTimer;
      
    public SM_Combat_Idle(CombatScript combat)
    {
        this.combat = combat;
    }

    public void Exit()
    {
        
    }

    public void FixedUpdate()
    {
        
    }

    public void Start()
    {
        //Validate that the idle statea has been entered
        Debug.Log("entered idle");
        //set cooldown timers to the cooldown timers in the CombatData class
        lTimer = combat.combatData.attackCooldown;
        hTimer = combat.combatData.heavyCooldown;

        
    }
    public void Update()
    {
        //start the timers using real (delta) time
        lTimer -= Time.deltaTime;
        hTimer -= Time.deltaTime;
        if (lTimer <= 0)
        {
            //all attacks work the same, use the SetState method to change the state to whatever attack is called depending on the conditions of the If statements
                //Light Attacks
            if (Input.GetButtonDown("LightAttack") && combat.movement.isGrounded == true ) 
            {
                if (Input.GetAxisRaw("Vertical") < 0)
                {
                    combat.SetState(new SM_Combat_DLight(combat));
                    combat.isSideAttacking = false;
                    combat.isDownAttacking = true;
                    combat.isNeutralAttacking = false;
                }

                else if (Input.GetAxisRaw("Horizontal") != 0)
                {
                    combat.SetState(new SM_Combat_SLight(combat));
                    combat.isSideAttacking = true;
                    combat.isDownAttacking = false;
                    combat.isNeutralAttacking = false;
                }

                else
                {
                    combat.SetState(new SM_Combat_NLight(combat));
                    combat.isSideAttacking = false;
                    combat.isDownAttacking= false;
                    combat.isNeutralAttacking = true;
                }

            }

            //air attacks
            if (Input.GetButtonDown("LightAttack") && combat.movement.isGrounded == false)
            {
                
                if (Input.GetAxisRaw("Vertical") < 0)
                {
                    combat.SetState(new SM_Combat_DAir(combat));
                    combat.isSideAttacking = false;
                    combat.isDownAttacking = true;
                    combat.isNeutralAttacking = false;
                }

                else if (Input.GetAxisRaw("Horizontal") != 0)
                {
                    combat.SetState(new SM_Combat_SAir(combat));
                    combat.isSideAttacking = true;
                    combat.isDownAttacking = false;
                    combat.isNeutralAttacking = false;
                }
                else
                {
                    combat.SetState(new SM_Combat_NAir(combat));
                    combat.isSideAttacking = false;
                    combat.isDownAttacking = false;
                    combat.isNeutralAttacking = true;
                }
                
            }
        }
        //Heavy Attacks (only happen if the cooldown is over

        if(hTimer <= 0)
        {
            if(Input.GetButtonDown("HeavyAttack") && combat.movement.isGrounded == true)
            {
                if(Input.GetAxisRaw("Horizontal") != 0)
                {
                    combat.SetState(new SM_Combat_SHeavy(combat));
                    combat.isSideHeavyAttacking = true;
                    combat.isNeutralHeavyAttacking = false;
                }
                else
                {
                    combat.SetState(new SM_Combat_NHeavy(combat));
                    combat.isSideHeavyAttacking = false;
                    combat.isNeutralHeavyAttacking = true;
                }
                
            }
        }

    }



   



}
