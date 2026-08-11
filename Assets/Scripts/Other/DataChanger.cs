using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataChanger : MonoBehaviour
{
    // References to the player and enemy movement scripts.
    private PlatMovement _player;
    private Plat2Movement _enemy;

    // Array to store different player types' data.
    [SerializeField] private Data[] playerTypes;

    // Index to track the current player type.
    private int _currentPlayerTypeIndex;

    // Flags to manage when the data change should occur.
    private bool dataChange = false;
    private bool hasChanged = false;

    private void Awake()
    {
        // Find the player and enemy GameObjects by their tags and get their movement components.
        _player = GameObject.FindWithTag("Player").GetComponent<PlatMovement>();
        _enemy = GameObject.FindWithTag("Enemy").GetComponent<Plat2Movement>();
    }

    private void Start()
    {
        // Initialize by setting the player and enemy to the first type of data.
        SwitchPlayerType(0);
    }

    private void Update()
    {
        // If a data change is triggered and hasn't been processed yet.
        if (dataChange && !hasChanged)
        {
            // Switch to the second type of data (index 1).
            SwitchPlayerType(1);

            // Mark the change as processed.
            hasChanged = true;

            // Reset the data change flag.
            dataChange = false;
        }
    }

    // This method is triggered when an object exits the trigger collider.
    private void OnTriggerExit2D(Collider2D other)
    {
        // Log the name of the object that exited the trigger.
        Debug.Log("Collision Detected with: " + other.name);

        // Check if the object is either the player or the enemy and is not a trigger collider.
        if (other.CompareTag("Player") && !other.isTrigger || other.CompareTag("Enemy") && !other.isTrigger)
        {
            // Log that the correct object collided and conditions were met.
            Debug.Log("Player collided and trigger condition met");

            // Set the data change flag to true, indicating that a change should happen.
            dataChange = true;
        }
    }

    // Switches the player and enemy data to the specified type based on the index.
    private void SwitchPlayerType(int index)
    {
        // Assign the new data type to both the player and the enemy.
        _player.data = playerTypes[index];
        _currentPlayerTypeIndex = index;

        _enemy.data = playerTypes[index];
        _currentPlayerTypeIndex = index;
    }
}
