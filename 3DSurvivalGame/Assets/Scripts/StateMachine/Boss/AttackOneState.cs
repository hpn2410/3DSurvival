using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackOneState : StateMachineBehaviour
{
    float delayTime = 3f;
    float timer = 0f;
    int maxLoop = 5;
    int lastLoop = 0;
    bool isFinished = false;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer = 0f;
        lastLoop = 0;
        isFinished = false;
        animator.speed = 0.8f;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
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
}
