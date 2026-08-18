using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lazarus_Walk : StateMachineBehaviour
{
    // This was created by Christopher Bunnell
    // I don't remember the exact date I did this
    // So I'm just going to say the middle of April 2024

    public float speed = 2.5f;
    public float attackRange = 50f;

    private Transform player;
    private Rigidbody2D rb;
    private Looking_At_Player look;

    // OnStateEnter is called when a transition starts and
    // the state machine starts to evaluate this state.
    override public void OnStateEnter(
        Animator animator,
        AnimatorStateInfo stateInfo,
        int layerIndex)
    {
        look = animator.GetComponent<Looking_At_Player>();
        rb = animator.GetComponent<Rigidbody2D>();

        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

        if (playerObject != null)
        {
            player = playerObject.transform;
        }
        else
        {
            player = null;
        }
    }

    // OnStateUpdate is called every frame while this state is active.
    override public void OnStateUpdate(
        Animator animator,
        AnimatorStateInfo stateInfo,
        int layerIndex)
    {
        // The player may have been destroyed after this state started.
        // If so, stop executing the enemy movement code.
        if (player == null)
        {
            return;
        }

        if (rb == null)
        {
            return;
        }

        if (look != null)
        {
            look.LookAtPlayer();
        }

        Vector2 target = new Vector2(
            player.position.x,
            rb.position.y
        );

        Vector2 newPos = Vector2.MoveTowards(
            rb.position,
            target,
            speed * Time.fixedDeltaTime
        );

        rb.MovePosition(newPos);

        if (Vector2.Distance(player.position, rb.position) <= attackRange)
        {
            Debug.Log("Beginning Attack");
            animator.SetTrigger("StartAttack");
        }
    }

    // OnStateExit is called when a transition ends.
    override public void OnStateExit(
        Animator animator,
        AnimatorStateInfo stateInfo,
        int layerIndex)
    {
        animator.ResetTrigger("StartAttack");
    }
}