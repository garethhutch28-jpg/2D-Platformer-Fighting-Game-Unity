using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SM_Combat2_Idle : ICombatSM2
{
    private CombatScript2 combat;
    private float lTimer;
    private float hTimer;

    public SM_Combat2_Idle(CombatScript2 combat)
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
        Debug.Log("entered idle");
        lTimer = combat.combatData.attackCooldown;
        hTimer = combat.combatData.heavyCooldown;


    }
    public void Update()
    {
        lTimer -= Time.deltaTime;
        hTimer -= Time.deltaTime;
        if (lTimer <= 0)
        {
            //Light Attacks
            if (Input.GetButtonDown("LightAttack2") && combat.movement2.isGrounded == true)
            {
                if (Input.GetAxisRaw("Vertical2") < 0)
                {
                    combat.SetState(new SM_Combat2_DLight(combat));
                }

                else if (Input.GetAxisRaw("Horizontal2") != 0)
                {
                    combat.SetState(new SM_Combat2_SLight(combat));
                }

                else
                {
                    combat.SetState(new SM_Combat2_NLight(combat));
                }

            }

            //air attacks
            if (Input.GetButtonDown("LightAttack2") && combat.movement2.isGrounded == false)
            {

                if (Input.GetAxisRaw("Vertical2") < 0)
                {
                    combat.SetState(new SM_Combat2_DAir(combat));
                }

                else if (Input.GetAxisRaw("Horizontal2") != 0)
                {
                    combat.SetState(new SM_Combat2_SAir(combat));
                }
                else
                {
                    combat.SetState(new SM_Combat2_NAir(combat));
                }

            }
        }
        if (hTimer <= 0)
        {
            if (Input.GetButtonDown("HeavyAttack2") && combat.movement2.isGrounded == true)
            {
                if (Input.GetAxisRaw("Horizontal2") != 0)
                {
                    combat.SetState(new SM_Combat2_SHeavy(combat));
                }
                else
                {
                    combat.SetState(new SM_Combat2_NHeavy(combat));
                }

            }
        }





    }
}
