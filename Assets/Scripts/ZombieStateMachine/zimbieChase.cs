using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class zimbieChase : StateMachineBehaviour
{
    NavMeshAgent agent;
    Transform player;
    ZombieState zstate;

    public float chaseSpeed = 6f;

    public float stopChasingDIstance = 15;
    public float attackingDistance = 2f;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = animator.GetComponent<NavMeshAgent>();
        zstate = animator.GetComponent<ZombieState>();

        if (zstate.is_night)
        {
            chaseSpeed = 8.0f;
        }
        if (zstate.boss)
        {
            chaseSpeed = 4f;
            stopChasingDIstance = 150;
        }

        agent.speed = chaseSpeed;
    }


    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent.SetDestination(player.position);
        animator.transform.LookAt(player);

        float distanceformPlayer = Vector3.Distance(player.position, animator.transform.position);

        if(distanceformPlayer > stopChasingDIstance)
        {
            animator.SetBool("isChasing", false);
        }

        if(distanceformPlayer < attackingDistance)
        {
            animator.SetBool("isAttacking", true);
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent.SetDestination(agent.transform.position);
    }
}
