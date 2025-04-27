using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossChaseState : StateMachineBehaviour
{
    Transform player;
    NavMeshAgent agent;
    BossAttackSkillManager skillManager;
    int skillIndex;

    public float chaseSpeed = 6f;
    public float stopChasingDistance = 21f;
    public float attackingDistance;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.LogWarning("Chasing");

        player = GameObject.FindGameObjectWithTag("Player").transform;

        agent = animator.GetComponent<NavMeshAgent>();

        skillManager = animator.GetComponent<BossAttackSkillManager>();

        skillIndex = Random.Range(0, 2);

        agent.speed = chaseSpeed;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
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

        agent.SetDestination(player.position);
        animator.transform.LookAt(player);
        
        float distanceFromPlayer = Vector3.Distance(player.position, animator.transform.position);

        // ----- Check if the agent should stop chasing ----- //
        if (distanceFromPlayer > stopChasingDistance)
        {
            animator.SetBool("isChasing", false);
        }

        // ----- Check if the agent should attack ----- //
        if (distanceFromPlayer < attackingDistance)
        {
            if (skillIndex == 0)
            {
                animator.SetBool("Attack1", true);

            }
            else if (skillIndex == 1)
            {
                animator.SetBool("Attack2", true);
            }
        }

        
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent.SetDestination(agent.transform.position);
    }
}
