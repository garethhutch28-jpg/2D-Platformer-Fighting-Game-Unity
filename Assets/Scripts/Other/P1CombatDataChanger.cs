using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P1CombatDataChanger : MonoBehaviour
{
    public CombatData Brute;
    public CombatData Assasin;
    public CombatData AllRounder;

    public CombatScript player1;
    public CombatScript2 player2;
    public P1DamageManager damageManager1; // Reference to P1DamageManager

    public P1CharacterSelected P1CharacterSelected;
    public P2CharacterSelected P2CharacterSelected;

    public CombatData currentCombatData;

    private void Update()
    {
        if (P1CharacterSelected.Player1Brute == true)
        {
            ChangeCombatData(Brute);
            Debug.Log("Changed to Brute Combat Data");
        }
        else if (P1CharacterSelected.Player1Assasin == true)
        {
            ChangeCombatData(Assasin);
            Debug.Log("Changed to Assasin Combat Data");
        }
        else if (P1CharacterSelected.Player1AllRounder == true)
        {
            ChangeCombatData(AllRounder);
            Debug.Log("Changed to All Rounder Combat Data");
        }
    }

    private void ChangeCombatData(CombatData newCombatData)
    {
        currentCombatData = newCombatData;
        player1.combatData = currentCombatData;

        // Update the damageManager's combat data as well
        if (damageManager1 != null)
        {
            damageManager1.combatData = currentCombatData;
            damageManager1.currentHealth = currentCombatData.maxHealth; // Reset health to maxHealth of the new combat data
        }
    }
}
