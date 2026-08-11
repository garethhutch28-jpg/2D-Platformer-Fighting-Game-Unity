using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class P1DamageManager : MonoBehaviour
{
    public CombatData combatData;//Reference to combatdata class
    public float currentHealth; // Set to public so it can be updated when combatData changes
    public bool takingDamage = false;//check to see if damage is being taken
    public GameObject player1;//reference to player gameobject
    public bool player1dead = false;//check to see if player1 is dead
    public bool player2wins = false;//check to see if player1 has lost (used to trigger player 2 win screen)

    void Start()
    {
        //set health to max health
        currentHealth = combatData.maxHealth;
    }

    void Update()
    {
       
    }
    //take damage method
    public void TakeDamage(float damage)
    {
        //Remove damage from current health
        currentHealth -= damage;
        //set taking damage flag to true
        takingDamage = true;

        //if the player has less than 0 health, they are dead
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    //Die method
    public void Die()
    {
        //validate the player death in the console
        Debug.Log("dead");
        //set flag to true
        player1dead = true;
        //deactivate the player gameobject (so they disappear and cant move)
        player1.SetActive(false);
        //load player 2 win screen
        SceneManager.LoadSceneAsync(8);


    }
     
}
