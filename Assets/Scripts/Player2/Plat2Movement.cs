using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Plat2Movement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 moveInput;
    public Data data;
    public bool isGrounded;
    public LayerMask whatIsGround;
    public float checkRadius;
    public Transform feetPos;
    private float jumpTime;
    private bool isJumping;
    private bool isRunning = false;
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

    public Animator anim2;


    [SerializeField] private Transform wallCheck;
    [SerializeField] public LayerMask whatIsWall;
    public LayerMask dodgeLayer;
    public LayerMask enemyLayer;
    

    private void Start()
    {
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
        dodgeTimer -= Time.deltaTime;
        if (canMove == true)
        {
            DirectionFacingCheck();
            Jump();
            WallJump();
            WallSlide();


            if (Input.GetButtonDown("Dash") && canDash && isGrounded)
            {
                StartCoroutine(Dash());
            }
            if (Input.GetButtonDown("Dodge2") && dodgeTimer <= 0)
            {

                TriggerDodge();
            }

            if (rb.velocity.y > 0 || rb.velocity.y < 0 && isGrounded == false)
            {
                anim2.SetBool("AllRounderJumping", true);
            }
            else
            {
                anim2.SetBool("AllRounderJumping", false);
            }
            
        }
        

    }

    private void FixedUpdate()
    {
        if(canMove == true)
        {
            //Horizontal and Vertical Inputs
            moveInput.x = Input.GetAxisRaw("Horizontal2");
            moveInput.y = Input.GetAxisRaw("Vertical2");

            Flip();


            FastFall();
            if (!isWallJumping)
            {
                Run();
            }
            if (isRunning == true && isGrounded == true)
            {
                anim2.SetBool("AllRounderRunning", true);
            }
            else
            {
                anim2.SetBool("AllRounderRunning", false);
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
        if (isFacingRight && moveInput.x < 0f || !isFacingRight && moveInput.x > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
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

        if (Input.GetButtonDown("Jump2") && isJumping == true)
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
        if (Input.GetButtonUp("Jump2"))
        {
            isJumping = false;
        }
        if (Input.GetButtonDown("Jump2"))
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
        if (Input.GetButtonDown("Jump2") && availableJumps > 0)
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
        if (IsOnWall() && !isGrounded && moveInput.x != 0f)
        {
            isWallSliding = true;
            Debug.Log("ON WALL");
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -data.wallSlideSpeed, float.MaxValue));
            availableJumps = data.maxJumps;
        }
        else
        {
            isWallSliding = false;
        }
    }

    private void WallJump()
    {
        if (isWallSliding)
        {
            isWallJumping = false;
            wallJumpingDirection = -transform.localScale.x;
            wallJumpingCounter = data.wallJumpingTime;

            CancelInvoke(nameof(StopWallJump));
        }

        else
        {
            wallJumpingCounter -= Time.deltaTime;
        }
        if (Input.GetButtonDown("Jump2") && wallJumpingCounter > 0)
        {
            isWallJumping = true;
            rb.velocity = new Vector2(wallJumpingDirection * data.wallJumpForce.x, data.wallJumpForce.y);
            wallJumpingCounter = 0;
            if (transform.localScale.x != wallJumpingDirection)
            {
                isFacingRight = !isFacingRight;
                Vector3 localScale = transform.localScale;
                localScale.x *= -1f;
                transform.localScale = localScale;
            }

        }




        Invoke(nameof(StopWallJump), data.wallJumpingDuration);


    }
    private void StopWallJump()
    {
        isWallJumping = false;
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
        rb.velocity = new Vector2(movement1.transform.localScale.x * 3, 0);
    }
    public void SLightKnockBack()
    {
        rb.velocity = new Vector2(movement1.transform.localScale.x * 10, 0);
    }
    public void DLightKnockBack()
    {
        rb.velocity = new Vector2(movement1.transform.localScale.x * 2, 2);
    }
    public void NAirKnockBack()
    {
        rb.velocity = new Vector2(movement1.transform.localScale.x * 1, 7);
    }
    public void SAirKnockBack()
    {
        rb.velocity = new Vector2(movement1.transform.localScale.x * 10, 0);
    }
    public void DAirKnockBack()
    {
        
        rb.velocity = new Vector2(movement1.transform.localScale.x * 2, -15);
    }
    public void NHeavyKnockBack()
    {
        rb.velocity = new Vector2(movement1.transform.localScale.x * 7, 3);
    }
    public void SHeavyKnockBack()
    {
        rb.velocity = new Vector2(movement1.transform.localScale.x * 20, 1);
    }

    public void TriggerDodge()
    {
        StartCoroutine(Dodge());
    }

    private IEnumerator Dodge()
    {

        dodgeTimer = dodgeCooldown;      
        canMove = false;
        Debug.Log ("canDamage" + canDamage);
        gameObject.layer = LayerMask.NameToLayer("Dodge");
       

        yield return new WaitForSeconds(0.48f);

        
        
        canMove = true;
        gameObject.layer = LayerMask.NameToLayer("Enemy");

        Debug.Log("canDamage" + canDamage);
        
        
        yield return new WaitForSeconds(1.75f);
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(feetPos.position, checkRadius);

    }
}
