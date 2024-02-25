/*
 * ****************************************************************************** *
 * Last Modified by Bobby Lapadula                                                *
 * Date and Time: 2/8/2024 02:11                                                  *
 * ****************************************************************************** *
*/
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BetterJump : MonoBehaviour
{
    [SerializeField] private float fallMultiplier = 2.5f;
    [SerializeField] private float smallJump = 2f;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.fixedDeltaTime;
        }
        else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (smallJump - 1) * Time.fixedDeltaTime;
        }
    }
}
