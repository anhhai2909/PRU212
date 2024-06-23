using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class idleBehaviour : StateMachineBehaviour
{
    public float timer;
    public int count=0;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        SetRandomTrigger(animator);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Check current health
        int currentHealth = animator.GetComponent<NecromancerController>().currentHealth;
        Debug.Log(currentHealth);
        if (currentHealth < 200 && count==0)
        {
            count++;
            //animator.SetTrigger("spikeSpawn");
            animator.SetBool("isLowHealth", true);
        }
        if (timer <= 0)
        {
            SetRandomTrigger(animator);
        }
        else
        {
            timer -= Time.deltaTime;
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("skeleton");
        animator.ResetTrigger("shoot");
    }

    private void SetRandomTrigger(Animator animator)
    {
        int rand = Random.Range(0, 3);
        if (rand == 0)
        {
            animator.SetTrigger("skeleton");
        }
        else
        {
            animator.SetTrigger("shoot");
        }
    }
}
