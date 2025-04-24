using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTwoState : StateMachineBehaviour
{
    float delayTime = 1f;
    float timer = 0f;
    bool delayed = false;

    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer = 0f;
        delayed = false;
    }

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
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

    }
}
