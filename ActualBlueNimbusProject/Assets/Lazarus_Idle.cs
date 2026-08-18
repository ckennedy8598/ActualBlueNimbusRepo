using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lazarus_Idle : StateMachineBehaviour
{
    public float sightRange = 60f;

    Transform player;
    Rigidbody2D rb;

    // OnStateEnter is called when a transition starts
    // and the state machine starts to evaluate this state.
    override public void OnStateEnter(
        Animator animator,
        AnimatorStateInfo stateInfo,
        int layerIndex)
    {
        rb = animator.GetComponent<Rigidbody2D>();

        FindPlayer();
    }

    // OnStateUpdate is called on each Update frame
    // between OnStateEnter and OnStateExit callbacks.
    override public void OnStateUpdate(
        Animator animator,
        AnimatorStateInfo stateInfo,
        int layerIndex)
    {
        // The old Player may have been destroyed.
        // Try to find a new Player if one exists.
        if (player == null)
        {
            FindPlayer();

            if (player == null)
            {
                return;
            }
        }

        if (rb == null)
        {
            return;
        }

        if (Vector2.Distance(player.position, rb.position) <= sightRange)
        {
            Debug.Log("Player Spotted");
            animator.SetTrigger("StartWalk");
        }
    }

    // OnStateExit is called when a transition ends
    // and the state machine finishes evaluating this state.
    override public void OnStateExit(
        Animator animator,
        AnimatorStateInfo stateInfo,
        int layerIndex)
    {
        animator.ResetTrigger("StartWalk");
    }

    private void FindPlayer()
    {
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
}