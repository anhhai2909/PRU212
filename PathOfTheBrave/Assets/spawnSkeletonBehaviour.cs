using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class spawnSkeletonBehaviour : StateMachineBehaviour
{
    public int rand;
    public float timer;
    public GameObject necro;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //if (timer <= 0)
        //{
        //    rand = Random.Range(0, 2);
        //    if (rand == 0)
        //    {
        //        animator.SetTrigger("idle");
        //    }
        //    else
        //    {
        //        animator.SetTrigger("shoot");
        //    }
        //}
        //else
        //{
        //    timer -= Time.deltaTime;
        //}
        animator.SetBool("idle", true);
        animator.SetBool("skeleton", false);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("idle", true);
        animator.SetBool("skeleton", false);
        OnDestroy();
    }

    public void OnDestroy()
    {
        DestroyImmediate(necro, true);
    }
}
