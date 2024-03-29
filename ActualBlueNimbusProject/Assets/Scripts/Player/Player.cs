/*
 * ****************************************************************************** *
 * Last Modified by Bobby Lapadula                                                *
 * Date and Time: 3/18/2024 15:12                                                 *
 *                                                                                *
 * This is the player movement script. It contains everything necessary for the   *
 * player to move properly including direction movement, jumping and double       *
 * jumping, dashing, and quick dropping.                                          *
 * ****************************************************************************** *
*/
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player Combat Script Reference")]
    public PlayerCombat PlayerHealth;

    [Header ("Player Body Reference")]
    private Rigidbody2D rb;
    private BoxCollider2D coll;

    [Header("Animation Variables")]
    private Animator anim;
    private SpriteRenderer sprite;

    [Header("Jumping Variables")]
    [SerializeField] private float downForce;
    [SerializeField] private int moveSpeedModifier = 1;
    [SerializeField] private LayerMask jumpableGround;
    [SerializeField] private LayerMask jumpableWalls;

    [Header ("Movement and Jumping")]
    private float horizontalInput = 0f;
    private bool quickDrop = true;
    private bool doubleJumpCheck = true;
    private bool doubleJump;
    private bool jumpKeyPressed;
    private bool downKey;

    [Header ("Wall Sliding")]
    [SerializeField] private Transform frontCheck;
    [SerializeField] private float wallSlidingSpeed;
    private bool facingRight = true;
    private bool isTouchingFront;
    private bool wallSliding;
    
    [Header ("Circle radius for front check")]
    private float checkRadius = .04f;

    [Header ("Wall Jumping")]
    [SerializeField] private float xWallForce;
    [SerializeField] private float yWallForce;
    [SerializeField] private float wallJumpTime;
    private bool wallJumping;

    [Header ("Dashing")]
    [SerializeField] private float dashSpeed = 42f;
    [SerializeField] private float dashTime = .2f;
    [SerializeField] private float dashCooldown = 1f;
    private bool canDash = true;
    private bool isDashing;

    [Header ("Sound Effects")]
    [SerializeField] private AudioSource backgroundMusic;
    [SerializeField] private AudioSource jumpSoundEffect;
    [SerializeField] private AudioSource dashSoundEffect;

    private int debugCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        PlayerHealth = FindObjectOfType<PlayerCombat>();

        // Initialize variables on start
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        backgroundMusic.Play();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        if (gameObject == null)
        {
            return;
        }

        // return out of Update method if dashing
        if (isDashing)
        {
            return;
        }

        // Jumping
        if (Input.GetKeyDown("space") && IsGround())
        {
            jumpKeyPressed = true;
            jumpSoundEffect.Play();
        }
        else if (Input.GetKeyDown("space") && !IsGround() && doubleJumpCheck)
        {
            //Debug.Log("In the Air.");
            doubleJumpCheck = false;
            doubleJump = true;
            jumpSoundEffect.Play();
        }
        
        // Quick Drop Input - Hard coded to S. Change in the future.
        if (Input.GetKeyDown(KeyCode.S) && !IsGround() && quickDrop && !wallSliding)
        {
            downKey = true;
            //Debug.Log("S key is true.");
        }

        // Quick Drop Ground Check
        if (IsGround() || wallSliding)
        {
            quickDrop = true;
            doubleJumpCheck = true;
        }

        // Wall Jumping Check
        if (Input.GetKeyDown("space") && wallSliding)
        {
            Invoke("SetWallJumpingToFalse", wallJumpTime);
            wallJumping = true;
            jumpSoundEffect.Play();
        }
        
        // Dashing Check
        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            dashSoundEffect.Play();
            StartCoroutine(Dash());
        }
    }

    // Better to put physics logic here
    private void FixedUpdate()
    {
        // Debug check for variable status
        //Debug.Log("Quick Drop: " + quickDrop + "- Down Key: " + downKey);

        // return out of Update method if dashing
        if (isDashing)
        {
            return;
        }

        // Velocity Clamp
        rb.velocity = Vector2.ClampMagnitude(rb.velocity, 16);

        // Jumping
        if (doubleJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, 7);
            doubleJump = false;
        }
        else if (jumpKeyPressed)
        {
            rb.velocity = new Vector2(rb.velocity.x, 7);
            jumpKeyPressed = false;
        }

        // Horizontal Movement
        rb.velocity = new Vector2(horizontalInput * moveSpeedModifier, rb.velocity.y);

        // Wall Front Check
        isTouchingFront = Physics2D.OverlapCircle(frontCheck.position, checkRadius, jumpableWalls);

        if (wallJumping)
        {
            rb.velocity = new Vector2(xWallForce * -horizontalInput, yWallForce);
        }

        // Quick Drop Logic
        // Inconsistent due to upward velocity varying.
        if (downKey)
        {
            rb.velocity = new Vector2(rb.velocity.x, downForce);
            downKey = false;
            quickDrop = false;
            //Debug.Log("S Key Pressed.");
        }

        if (isTouchingFront && !IsGround() && horizontalInput != 0)
        {
            wallSliding = true;
            anim.SetBool("wall_sliding", true);
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlidingSpeed, float.MaxValue));
        }
        else
        {
            wallSliding = false;
            anim.SetBool("wall_sliding", false);
        }

        UpdateAnimationState();
    }

    // Flips and changes sprite animations
    private void UpdateAnimationState()
    {
        // Flipping Sprite + Animation
        if (horizontalInput > 0f && !wallSliding)
        {
            anim.SetBool("running", true);
            if (!facingRight)
            {
                Flip();
            }
        }
        else if (horizontalInput < 0f && !wallSliding)
        {
            anim.SetBool("running", true);
            if (facingRight)
            {
                Flip();
            }
        }
        else
        {
            anim.SetBool("running", false);
        }
    }

    // On Ground Method Check
    private bool IsGround()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }

    // Flip Sprite Left and Right Method
    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }

    private void SetWallJumpingToFalse()
    {
        wallJumping = false;
    }

    // Dashing Coroutine
    // Coroutines are very important for spreading an action over several frames, like dashing or animations.
    // We'll be using these often, it seems, so definitely read up on them.
    // Video for reference - https://youtu.be/2kFGmuPHiA0
    // Unity documentation for coroutines - https://docs.unity3d.com/Manual/Coroutines.html
    private IEnumerator Dash()
    {
        PlayerHealth.CanBeHit();
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(transform.localScale.x * dashSpeed, 0f);
        yield return new WaitForSeconds(dashTime);
        PlayerHealth.CanBeHit();
        //Debug.Log("State of CanBeHit: " PlayerHealth.CanBeHit());
        rb.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }
}
