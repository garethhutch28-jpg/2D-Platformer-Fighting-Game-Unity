using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class PlatMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 moveInput;
    public Data data;
    public CombatScript combat1;
    public bool isGrounded;
    public LayerMask whatIsGround;
    public float checkRadius;
    public Transform feetPos;
    private float jumpTime;
    public bool isRunning = false;
    public bool isJumping;
    private float coyoteTime = 0f;
    private float coyoteTimeCounter;
    private float jumpBufferTime = 0f;
    private float jumpBufferTimer;
    private int availableJumps;
    public float gravity = 1f;
    private bool canDash = true;
    private bool isDashing;
    private bool isFacingRight = true;
    private bool isWallSliding;

    private bool isWallJumping;
    private float wallJumpingDirection;
    private float wallJumpingCounter;

    private float knockBackTimer = 0.5f;
    private bool isKnockedBack;
    //if true facing right if false facing left
    private bool facingDirection = true;
    public PlatMovement movement1;
    public Plat2Movement movement2;

    public bool canDamage = true;
    private bool canMove = true;
    private float dodgeTimer;
    private float dodgeCooldown = 1.75f;


    [SerializeField] private Transform wallCheck;
    [SerializeField] public LayerMask whatIsWall;
    public LayerMask dodgeLayer;
    public LayerMask enemyLayer;

    private const int UPPERBODY = 0;
    private const int LOWERBODY = 1;


    private void Start()
    {
        //Initialize(GetComponent<Animator>().layerCount, Animations.IDLE, GetComponent<Animator>(), DefaultAnimation);
        dodgeTimer = 0;
    }

    private void Awake()
    {
        //get a reference to the rigid body on the character, allowing it to interact with physiscs
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        bool isPlayerRunning = isRunning && isGrounded;

        dodgeTimer -= Time.deltaTime;
        if (canMove == true)
        {
            DirectionFacingCheck();
            Jump();
            if (rb.velocity.y > 0 || rb.velocity.y < 0 && isGrounded == false)
            {

            }
            else
            {

            }


            WallJump();
            WallSlide();



            if (Input.GetButtonDown("Dash") && canDash && isGrounded)
            {
                StartCoroutine(Dash());
            }
            if (Input.GetButtonDown("Dodge") && dodgeTimer <= 0)
            {

                TriggerDodge();
            }
        }
        Debug.Log(movement2);
        Debug.Log(rb);
     
        if (combat1.isSideAttacking == true)
        {

        }
        if (combat1.isDownAttacking == true)
        {
            Debug.Log("downlight");
        }
        if (combat1.isNeutralAttacking == true)
        {
            Debug.Log("neutrallight");
        }
        if (combat1.isSideHeavyAttacking == true)
        {
            Debug.Log("sideheavy");
        }
        if (combat1.isNeutralHeavyAttacking == true)
        {
            Debug.Log("neutralheavy");
        }
        if (isRunning && isGrounded)
        {
            Debug.Log("running");
        }
        else
        {
            Debug.Log("jumping?");
        }


    }

    private void FixedUpdate()
    {
        if (canMove == true)
        {
            //Horizontal and Vertical Inputs
            moveInput.x = Input.GetAxisRaw("Horizontal");
            moveInput.y = Input.GetAxisRaw("Vertical");

            Flip();


            FastFall();
            if (!isWallJumping)
            {
                Run();

            }




            Debug.Log(isGrounded);

        }
    }

    private void Run()
    {
        // Calculate target speed based on input
        float targetSpeed = moveInput.x * data.maxSpeed;
        float speedDifference = targetSpeed - rb.velocity.x;

        // Set acceleration rate based on whether we're accelerating or decelerating
        float accelerationRate = (Mathf.Abs(targetSpeed) > 0.01f) ? data.acceleration : data.deceleration;
        float movement = speedDifference * accelerationRate;

        // Apply movement if there is input
        if (Mathf.Abs(moveInput.x) > 0.01f)
        {
            rb.AddForce(movement * Vector2.right, ForceMode2D.Force);
            isRunning = true;
        }
        else
        {
            // Apply deceleration when there is no input
            if (Mathf.Abs(rb.velocity.x) > 0.01f) // If velocity is still significant
            {
                float decelerationForce = -rb.velocity.x * data.deceleration;
                rb.AddForce(decelerationForce * Vector2.right, ForceMode2D.Force);
            }

            isRunning = false;
        }
    }

    private void Flip()
    {
        //Condions for when a flip is needed, EG: character is facing right but is moving left
        if (isFacingRight && moveInput.x < 0f || !isFacingRight && moveInput.x > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            //flip local scale
            localScale.x *= -1f;
            //apply it to the players transform
            transform.localScale = localScale;
        }
    }



    private void Jump()
    {
        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);

        if (isGrounded)
        {
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

        if (coyoteTimeCounter > 0f && jumpBufferTimer > 0f)
        {
            isJumping = true;
            jumpTime = data.jumpStartTime;
            //rb.velocity = Vector2.up * data.jumpForce;
            float force = data.jumpForce;
            force -= rb.velocity.y;
            rb.AddForce(Vector2.up * force, ForceMode2D.Impulse);
            jumpBufferTimer = 0f;
        }

        if (Input.GetButtonDown("Jump") && isJumping == true)
        {
            coyoteTimeCounter = 0;
            if (jumpTime > 0)
            {
                float force = data.jumpForce;
                force -= rb.velocity.y;
                rb.AddForce(Vector2.up * force, ForceMode2D.Impulse);
                jumpTime -= Time.deltaTime;

            }
            else
            {
                isJumping = false;
            }


        }
        if (Input.GetButtonUp("Jump"))
        {
            isJumping = false;
        }
        if (Input.GetButtonDown("Jump"))
        {
            jumpBufferTimer = jumpBufferTime;
        }
        else
        {
            jumpBufferTimer -= Time.deltaTime;
        }

        if (isGrounded == true && isJumping == false)
        {
            availableJumps = data.maxJumps;
        }
        if (Input.GetButtonDown("Jump") && availableJumps > 0)
        {
            isJumping = true;
            availableJumps = availableJumps - 1;
            float force = data.jumpForce;
            force -= rb.velocity.y;
            rb.AddForce(Vector2.up * force, ForceMode2D.Impulse);
        }

    }



    private void FastFall()
    {

        if (moveInput.y < 0)
        {
            rb.gravityScale = gravity * data.fastFallMultiplier;
            Debug.Log("fastfalling");
        }
        else
        {
            rb.gravityScale = gravity;
        }
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(transform.localScale.x * data.dashSpeed, 0f);
        yield return new WaitForSeconds(data.dashTimer);
        rb.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(data.dashCooldown);
        canDash = true;

    }

    private bool IsOnWall()
    {
        return Physics2D.OverlapCircle(wallCheck.position, 0.2f, whatIsWall);
    }

    private void WallSlide()
    {
        // Check if the character is on a wall, not grounded, and moving horizontally.
        if (IsOnWall() && !isGrounded && moveInput.x != 0f)
        {
            isWallSliding = true; // Enable wall sliding.
            Debug.Log("ON WALL"); // Debug message.

            // Control descent speed while sliding on the wall.
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -data.wallSlideSpeed, float.MaxValue));

            availableJumps = data.maxJumps; // Reset jumps for wall jumping.
        }
        else
        {
            isWallSliding = false; // Disable wall sliding.
        }
    }

    private void WallJump()
    {
        if (isWallSliding)
        {
            isWallJumping = false; // Reset wall jumping state.
            wallJumpingDirection = -transform.localScale.x; // Set jump direction opposite to current facing direction.
            wallJumpingCounter = data.wallJumpingTime; // Reset wall jump timer.

            CancelInvoke(nameof(StopWallJump)); // Cancel any pending StopWallJump invocation.
        }
        else
        {
            wallJumpingCounter -= Time.deltaTime; // Decrease wall jump timer.
        }

        if (Input.GetButtonDown("Jump") && wallJumpingCounter > 0)
        {
            isWallJumping = true; // Trigger wall jump.
            rb.velocity = new Vector2(wallJumpingDirection * data.wallJumpForce.x, data.wallJumpForce.y); // Apply wall jump force.
            wallJumpingCounter = 0; // Reset the wall jump counter.

            // Flip the character's direction if necessary.
            if (transform.localScale.x != wallJumpingDirection)
            {
                isFacingRight = !isFacingRight;
                Vector3 localScale = transform.localScale;
                localScale.x *= -1f;
                transform.localScale = localScale;
            }
        }

        Invoke(nameof(StopWallJump), data.wallJumpingDuration); // Stop wall jumping after the duration ends.
    }

    private void StopWallJump()
    {
        isWallJumping = false; // End wall jumping state.
    }

    private void DirectionFacingCheck()
    {
        if (isFacingRight == true)
        {

            facingDirection = true;
        }
        else if (!isFacingRight)
        {

            facingDirection = false;
        }
    }

    public void NLightKnockBack()
    {
        rb.velocity = new Vector2(movement2.transform.localScale.x * 3, 0);
    }
    public void SLightKnockBack()
    {
        rb.velocity = new Vector2(movement2.transform.localScale.x * 10, 0);
    }
    public void DLightKnockBack()
    {
        rb.velocity = new Vector2(movement2.transform.localScale.x * 2, 2);
    }
    public void NAirKnockBack()
    {
        rb.velocity = new Vector2(movement2.transform.localScale.x * 1, 7);
    }
    public void SAirKnockBack()
    {
        rb.velocity = new Vector2(movement2.transform.localScale.x * 10, 0);
    }
    public void DAirKnockBack()
    {

        rb.velocity = new Vector2(movement2.transform.localScale.x * 2, -15);
    }
    public void NHeavyKnockBack()
    {
        rb.velocity = new Vector2(movement2.transform.localScale.x * 7, 3);
    }
    public void SHeavyKnockBack()
    {
        rb.velocity = new Vector2(movement2.transform.localScale.x * 20, 1);
    }

    public void TriggerDodge()
    {
        StartCoroutine(Dodge());
    }

    private IEnumerator Dodge()
    {

        dodgeTimer = dodgeCooldown;
        canMove = false;
        //debuggin n shi like that brev ya feeeeeel meh?
        Debug.Log("canDamage" + canDamage);
        gameObject.layer = LayerMask.NameToLayer("Dodge");


        yield return new WaitForSeconds(0.48f);



        canMove = true;
        gameObject.layer = LayerMask.NameToLayer("Player");

        Debug.Log("canDamage" + canDamage);


        yield return new WaitForSeconds(1.75f);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(feetPos.position, checkRadius);

    }
    /*
    private void CheckTopAnimation()
    {
        CheckMovementAnimations(UPPERBODY);
    }
    private void CheckBottomAnimation()
    {
        CheckMovementAnimations(LOWERBODY);
    }
    private void CheckMovementAnimations(int layer)
    {

        if (moveInput.x > 0 && !isWallJumping && !isWallSliding && !isGrounded)
        {
            Play(Animations.RUN, layer, false, false);
        }
        else if (moveInput.x < 0 && !isWallJumping && !isWallSliding)
        {
            Play(Animations.RUN, layer, false, false);
        }
        else if (!isGrounded)
        {
            Play(Animations.JUMP, layer, false, false);
        }
        else
        {
            Play(Animations.IDLE, layer, false, false);
        }

    }
    void DefultAnimation(int layer)
    {
        if (layer == UPPERBODY)
        {
            CheckTopAnimation();
        }
        else
        {
            CheckBottomAnimation();
        }
    }
    private void CheckTopAnimation() => CheckMovementAnimations(UPPERBODY);
    private void CheckBottomAnimation() => CheckMovementAnimations(LOWERBODY);

    private void CheckMovementAnimations(int layer)
    {
        if (moveInput.x != 0 && !isWallJumping && !isWallSliding && !isGrounded)
        {
            Play(Animations.RUN, layer, false, false);
        }
        else if (!isGrounded)
        {
            Play(Animations.JUMP, layer, false, false);
        }
        else
        {
            Play(Animations.IDLE, layer, false, false);
        }
    }

    void DefaultAnimation(int layer)
    {
        if (layer == UPPERBODY)
            CheckTopAnimation();
        else
            CheckBottomAnimation();
    }

}
    */
}



