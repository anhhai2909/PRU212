using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spikeBehaviour : StateMachineBehaviour
{
    public GameObject spike;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //spike.SetActive(true);
        ////CheckAndTriggerSpike(animator);
        //int currentHealth = animator.GetComponent<NecromancerController>().currentHealth;
        //if (currentHealth < 200)
        //{
        //    Animator spikeAnimator = spike.GetComponent<Animator>();

        //    spikeAnimator.gameObject.SetActive(true);
        //    if (spikeAnimator != null)
        //    {
        //        spikeAnimator.SetBool("isActive",true);
        //        Debug.Log("Spike");
        //    }
        //}
        //else
        //{
        //    spike.SetActive(false);
        //}
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //CheckAndTriggerSpike(animator);
        //spike.SetActive(true);
    }

    private void CheckAndTriggerSpike(Animator animator)
    {
        
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (spike != null)
        {
            spike.SetActive(true);
        }
        animator.SetBool("isLowHealth",false); // Reset the isLowHealth flag
    }
}
