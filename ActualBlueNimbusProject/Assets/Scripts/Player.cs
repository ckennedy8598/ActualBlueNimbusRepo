/*
 * ****************************************************************************** *
 * Last Modified by Bobby Lapadula                                                *
 * Date and Time: 2/8/2024 22:45                                                  *
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
    [SerializeField] private float downForce;
    [SerializeField] private int moveSpeedModifier = 1;
    [SerializeField] private LayerMask jumpableGround; 
    [SerializeField] private LayerMask jumpableWalls; // Unused at the moment. Going to be used for wall jumping and sliding.

    
    private Rigidbody2D rb;
    private BoxCollider2D coll;

    private float horizontalInput;
    private bool jumpKeyPressed;
    private bool downKey;
    private bool quickDrop = true;

    // Wall Sliding
    [SerializeField] private Transform frontCheck;
    [SerializeField] private float wallSlidingSpeed;
    private bool facingRight = true;
    private bool isTouchingFront;
    private bool wallSliding;
    
    // Circle radius for front check
    private float checkRadius = .04f;

    // Wall Jumping
    [SerializeField] private float xWallForce;
    [SerializeField] private float yWallForce;
    [SerializeField] private float wallJumpTime;
    private bool wallJumping;

    // Sound Effects
    [SerializeField] private AudioSource jumpSoundEffect;
    [SerializeField] private AudioSource backgroundMusic;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize variables on start
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        backgroundMusic.Play();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        // Flipping Sprite
        if (facingRight == false && horizontalInput > 0)
        {
            Flip();
        }else if(facingRight == true && horizontalInput < 0)
        {
            Flip();
        }

        // Jumping
        if (Input.GetKeyDown("space") && IsGround())
        {
            jumpKeyPressed = true;
            jumpSoundEffect.Play();
        }
        
        // Quick Drop Input - Hard coded to S. Change in the future.
        if (Input.GetKeyDown(KeyCode.S) && !IsGround() && quickDrop && wallSliding == false)
        {
            downKey = true;
            //Debug.Log("S key is true.");
        }

        // Quick Drop Ground Check
        if (IsGround() || wallSliding == true)
        {
            quickDrop = true;
            //Debug.Log("Quick Drop: True.");
        }

        // Wall Jumping Check
        if (Input.GetKeyDown("space") && wallSliding)
        {
            wallJumping = true;
            Invoke("SetWallJumpingToFalse", wallJumpTime);
        }
        
    }

    // Better to put physics logic here
    private void FixedUpdate()
    {
        // Debug check for variable status
        //Debug.Log("Quick Drop: " + quickDrop + "- Down Key: " + downKey);

        // Velocity Clamp
        rb.velocity = Vector2.ClampMagnitude(rb.velocity, 16);

        // Jumping
        if (jumpKeyPressed)
        {
            rb.velocity = new Vector2(rb.velocity.x, 7);
            jumpKeyPressed = false;
        }

        // Horizontal Movement
        rb.velocity = new Vector2(horizontalInput * moveSpeedModifier, rb.velocity.y);

        // Wall Front Check
        isTouchingFront = Physics2D.OverlapCircle(frontCheck.position, checkRadius, jumpableWalls);

        if (isTouchingFront && !IsGround() && horizontalInput != 0)
        {
            wallSliding = true;
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlidingSpeed, float.MaxValue));
        }
        else
        {
            wallSliding = false;
        }

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
}
