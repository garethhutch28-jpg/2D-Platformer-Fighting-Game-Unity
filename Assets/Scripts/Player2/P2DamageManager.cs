using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class P2DamageManager : MonoBehaviour
{
    public CombatData combatData; // Reference to combatdata class
    public float currentHealth; // Set to public so it can be updated when combatData changes
    public bool takingDamage = false; // Check to see if damage is being taken
    public GameObject player2; // Reference to player gameobject
    public bool player2dead = false; // Check to see if player2 is dead
    public bool player1wins = false; // Check to see if player2 has lost (used to trigger player 1 win screen)

    void Start()
    {
        // Set health to max health
        currentHealth = combatData.maxHealth;
    }

    void Update()
    {
        // WinDecider();
    }

    // Take damage method
    public void TakeDamage(float damage)
    {
        // Remove damage from current health
        currentHealth -= damage;
        // Set taking damage flag to true
        takingDamage = true;

        // If the player has less than 0 health, they are dead
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // Die method
    public void Die()
    {
        // Validate the player death in the console
        Debug.Log("ded");
        // Set flag to true
        player2dead = true;
        // Deactivate the player gameobject (so they disappear and can't move)
        player2.SetActive(false);
        // Load player 1 win screen
        SceneManager.LoadSceneAsync(9);
    }
}

