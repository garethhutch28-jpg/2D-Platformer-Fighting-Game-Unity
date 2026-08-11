using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()] 
public class Data : ScriptableObject
{
    [Header("Run")]

    public float maxSpeed; //Run speed after acceleration
    public float acceleration; //Acceleration
    public float deceleration; //Deceleration

    [Header("Jump")]
    public float jumpForce; //Force applied to character
    public float jumpStartTime; //Duration for variable jump to happen
    public int maxJumps; //Maximum number of jumps a player has
    public float fastFallMultiplier;//Added to the falling player to increase fall speed

    public float wallSlideSpeed;//Speed at which player falls on wall
    public float wallJumpingTime = 0.2f;//Time for wall jump to start
    public float wallJumpingDuration = 0.4f;//Duration of a wall jump
    public Vector2 wallJumpForce = new Vector2(8f, 16f); //Froce of wall jump

    [Header("Dash")]
    public float dashCooldown; //Dash cooldown
    public float dashSpeed; //Dash speed
    public float dashTimer; //Duration of the dash

    
    

}
