using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCombatData", menuName = "Combat/CombatData")] //Make a scriptable Object for this class
public class CombatData : ScriptableObject
{
    [Header("General")]
    public float attackCooldown = 1f; //Time player is not able to attack after attacking
    public float maxHealth; //Max health of each player

    //Each of these are the same for the 3 types of attacks
        //Range, Speed and Damage amounts all as floats
    [Header("Light Attacks")]
    public float lightAttackRange; 
    public float lightAttackSpeed;
    public float nLightAttackDamage;
    public float sLightAttackDamage;
    public float dLightAttackDamage;

    [Header("Air Attacks")]
    public float airAttackRange;
    public float nAirAttackDamage;
    public float sAirAttackDamage;
    public float dAirAttackDamage;

    [Header("Heavy Attacks")]
    public float heavyCooldown;
    public float nHeavyAttRange;
    public float nHeavyAttackDamage;
    public float sHeavyAttackDamage;

}
