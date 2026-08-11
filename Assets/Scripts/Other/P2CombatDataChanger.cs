using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P2CombatDataChanger : MonoBehaviour
{
    public CombatData Brute;
    public CombatData Assasin;
    public CombatData AllRounder;

    public CombatScript player1;
    public CombatScript2 player2;
    public P1DamageManager damageManager1;
    public P2DamageManager damageManager2; // Reference to P2DamageManager

    public P1CharacterSelected P1CharacterSelected;
    public P2CharacterSelected P2CharacterSelected;

    public CombatData currentCombatData;

    private void Update()
    {
        if (P2CharacterSelected.Player2Brute == true)
        {
            ChangeCombatData(Brute);
            Debug.Log("Changed to Brute Combat Data");
        }
        else if (P2CharacterSelected.Player2Assasin == true)
        {
            ChangeCombatData(Assasin);
            Debug.Log("Changed to Assasin Combat Data");
        }
        else if (P2CharacterSelected.Player2AllRounder == true)
        {
            ChangeCombatData(AllRounder);
            Debug.Log("Changed to All Rounder Combat Data");
        }
    }

    private void ChangeCombatData(CombatData newCombatData)
    {
        currentCombatData = newCombatData;
        player2.combatData = currentCombatData;

        // Update the damageManager's combat data as well
        if (damageManager2 != null)
        {
            damageManager2.combatData = currentCombatData;
            damageManager2.currentHealth = currentCombatData.maxHealth; // Reset health to maxHealth of the new combat data
        }
    }
}
