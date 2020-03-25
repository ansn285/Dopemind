using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodgeBehaviour : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    //override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!animator.GetComponent<CapsuleCollider2D>().enabled && !animator.GetComponent<PlayerMovement>().isFacingLeft)
        {
            animator.GetComponent<PlayerMovement>().enabled = false;

            animator.transform.position = new Vector2(animator.transform.position.x + 3 * Time.deltaTime, animator.transform.position.y);
        }

        else if (!animator.GetComponent<CapsuleCollider2D>().enabled && animator.GetComponent<PlayerMovement>().isFacingLeft)
        {
            animator.GetComponent<PlayerMovement>().enabled = false;

            animator.transform.position = new Vector2(animator.transform.position.x - 3 * Time.deltaTime, animator.transform.position.y);
        }

        else
        {
            animator.GetComponent<PlayerMovement>().enabled = true;
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

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
