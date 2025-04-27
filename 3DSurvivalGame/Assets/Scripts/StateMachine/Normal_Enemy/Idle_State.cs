using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle_State : StateMachineBehaviour
{
    float timer;
    public float idle_Time = 4f;
    Transform player;
    public float detectionAreaRadius = 18f;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer = 0;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(Player_State.Instance.isPlayerDead)
        {
            animator.SetBool("IsPlayerDead", true);
            animator.SetBool("isAttacking", false);
            animator.SetBool("isChasing", false);
        }
        else
        {
            animator.SetBool("IsPlayerDead", false);
        }

            // -------- Transition to Walk State -------- //
            timer += Time.deltaTime;
        if (timer > idle_Time)
        {
            animator.SetBool("isWalking", true);
        }

        // -------- Transition to Chase State -------- //
        float distanceFromPlayer = Vector3.Distance(player.position, animator.transform.position);

        if(distanceFromPlayer < detectionAreaRadius)
        {
            animator.SetBool("isChasing", true);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{

    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
