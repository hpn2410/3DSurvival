using System.Collections;
using System.Collections.Generic;
using Unity.Services.Analytics.Internal;
using UnityEngine;
using UnityEngine.AI;

public class AttackTwoState : StateMachineBehaviour
{
    float delayTime = 2f;
    float timer = 0f;
    bool delayed = false;

    Transform player;
    NavMeshAgent agent;

    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer = 0f;
        delayed = false;

        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = animator.GetComponent<NavMeshAgent>();

        animator.speed = .4f;
    }

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Player_State.Instance.isPlayerDead)
        {
            animator.SetBool("IsPlayerDead", true);
        }
        else
        {
            animator.SetBool("IsPlayerDead", false);
        }

        LookAtPlayer();

        if (!delayed && stateInfo.normalizedTime >= 1f) // Anim ended
        {
            timer += Time.deltaTime;
            if (timer >= delayTime)
            {
                animator.SetBool("Attack2", false);
                delayed = true;
            }
        }
    }

    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent.SetDestination(agent.transform.position);
    }

    private void LookAtPlayer()
    {
        Vector3 direction = player.position - agent.transform.position;
        agent.transform.rotation = Quaternion.LookRotation(direction);

        var yRotation = agent.transform.eulerAngles.y;
        agent.transform.rotation = Quaternion.Euler(0, yRotation, 0);
    }
}
