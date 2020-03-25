using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearMove : StateMachineBehaviour
{
    private Transform player;
    private Transform npc;

    public float speed;

    [System.NonSerialized] public bool inverse = false;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.Find("Genrad").transform;


        npc = GameObject.Find("Dopermine").transform;

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (inverse)
        {
            animator.GetComponent<SpriteRenderer>().flipX = false;
            animator.transform.position = Vector2.MoveTowards(animator.transform.position, npc.position, speed * Time.deltaTime);
        }

        else
        {
            animator.GetComponent<SpriteRenderer>().flipX = true;
            animator.transform.position = Vector2.MoveTowards(animator.transform.position, player.position, speed * Time.deltaTime);
        }

        if (animator.transform.position.x >= npc.position.x)
        {
            animator.gameObject.GetComponent<SpriteRenderer>().enabled = false;

            animator.gameObject.GetComponent<BoxCollider2D>().enabled = false;

            inverse = false;

            animator.GetComponent<SpriteRenderer>().flipX = true;

            GameObject.Find("/Dopermine/DoperHitBox").GetComponent<Damage>().TakeDamage(250);

            animator.transform.position = new Vector2(-9.029383f, -2.379735f);

            animator.SetBool("isFollowing", false);
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
