// HospitalChasing.cs
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HospitalChasing : StateMachineBehaviour
{
    NavMeshAgent agent;
    Transform player;

    public float chaseSpeed = 6f;
    public float attackingDistance = 2f;
    public float stopChasingDistance = 50f;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("isChasing", true);
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        agent = animator.GetComponent<NavMeshAgent>();

        if (agent != null)
        {
            agent.speed = chaseSpeed;
        }
        else
        {
            Debug.LogError("NavMeshAgent not found.");
        }
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (player == null)
        {
            Debug.LogError("Player not found.");
            return;
        }

        Zombie zombieComponent = animator.GetComponent<Zombie>();
        if (zombieComponent != null && zombieComponent.isIlluminated)
        {
            agent.SetDestination(player.position);
            animator.transform.LookAt(player);

            float distanceFromPlayer = Vector3.Distance(player.position, animator.transform.position);

            if (distanceFromPlayer < attackingDistance)
            {
                animator.SetBool("isAttacking", true);
            }
            else if (distanceFromPlayer > stopChasingDistance)
            {
                animator.SetBool("isChasing", false);
            }
        }
        else
        {
            // If not illuminated, return to patrolling state
            animator.SetBool("isChasing", false);
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (agent != null)
        {
            agent.SetDestination(agent.transform.position);
        }
    }
}
