using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class AttackingState : StateMachineBehaviour
{
    Transform player;
    NavMeshAgent agent;

    public float stopAttackingDistance;
    public float attackCooldown = 1.5f;
    private float lastAttackTime;
    

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = animator.GetComponent<NavMeshAgent>();

        lastAttackTime = Time.time;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
            LookAtPlayer();

        if (stateInfo.normalizedTime < 1f) return;

        if (Time.time > lastAttackTime + attackCooldown)
        {
            lastAttackTime = Time.time;

            float distanceFromPlayer = Vector3.Distance(player.position, animator.transform.position);

            if (distanceFromPlayer > stopAttackingDistance)
            {
                animator.SetBool("isAttacking", false);
            }
            else
            {
                animator.Play(stateInfo.fullPathHash, layerIndex, 0f);
            }
        }

        // ----- Check if agent should stop attacking ----- //
        //float distanceFromPlayer = Vector3.Distance(player.position, animator.transform.position);

        //if (distanceFromPlayer > stopAttackingDistance)
        //{
        //    animator.SetBool("isAttacking", false);
        //}

        //if (Time.time > lastAttackTime + attackCooldown)
        //{
        //    animator.Play(stateInfo.fullPathHash, layerIndex, 0f); // Replay current attack animation
        //    lastAttackTime = Time.time;
        //}
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent.SetDestination(agent.transform.position);
    }

    //public void TryAttack()
    //{
    //    float distance = Vector3.Distance(player.position, agent.transform.position);

    //    if (distance <= attackRange)
    //    {
    //        Player_State.Instance.TakeDamage(agent.GetComponent<NPCWaypoints>().enemyData.enemyDamage);
    //    }
    //    else
    //    {
    //        Debug.LogWarning("Dogde");
    //    }
    //}

    private void LookAtPlayer()
    {
        Vector3 direction = player.position - agent.transform.position;
        agent.transform.rotation = Quaternion.LookRotation(direction);

        var yRotation = agent.transform.eulerAngles.y;
        agent.transform.rotation = Quaternion.Euler(0, yRotation, 0);
    }

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
