using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shootBehaviour : StateMachineBehaviour
{
    public int rand;
    public float timer;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //fireballPos = GameObject.FindGameObjectWithTag("FireBallPos").transform;
        
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (timer <= 0)
        {
            rand = Random.Range(0, 2);
            if (rand == 0)
            {
                animator.SetTrigger("idle");
            }
            else
            {
                animator.SetTrigger("skeleton");
            }
        }
        else
        {
            timer -= Time.deltaTime;
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("idle");
        animator.ResetTrigger("skeleton");
    }
}
