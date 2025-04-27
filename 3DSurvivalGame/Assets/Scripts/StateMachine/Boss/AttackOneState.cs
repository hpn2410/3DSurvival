using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AttackOneState : StateMachineBehaviour
{
    float delayTime = 3f;
    float timer = 0f;
    int maxLoop = 3;
    int lastLoop = 0;
    bool isFinished = false;

    NavMeshAgent agent;
    Transform player;
    BossAttackSkillManager skillManager;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer = 0f;
        lastLoop = 0;
        isFinished = false;
        animator.speed = 0.8f;

        agent = animator.GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        skillManager = animator.GetComponent<BossAttackSkillManager>();
    }

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

        int currentLoop = Mathf.FloorToInt(stateInfo.normalizedTime);

        if (!isFinished && currentLoop > lastLoop)
        {
            lastLoop = currentLoop;

            if (lastLoop >= maxLoop)
            {
                isFinished = true;
                animator.speed = 0f;
            }
        }

        if (isFinished)
        {
            timer += Time.deltaTime;
            if (timer >= delayTime)
            {
                animator.speed = 1f;
                animator.SetBool("Attack1", false);
            }
        }
    }

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
