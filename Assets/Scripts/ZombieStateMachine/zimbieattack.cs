using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class zimbieattack : StateMachineBehaviour
{
    NavMeshAgent agent;
    Transform player;

    public float stopAttackingDistance = 2f;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = animator.GetComponent<NavMeshAgent>();
    }


    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        LookAtPlayer();

        float distanceformPlayer = Vector3.Distance(player.position, animator.transform.position);

        if (distanceformPlayer > stopAttackingDistance)
        {
            animator.SetBool("isAttacking", false);
        }
    }

    private void LookAtPlayer()
    {
        Vector3 direc = player.position -agent.transform.position;
        agent.transform.rotation = Quaternion.LookRotation(direc);

        var yRotation = agent.transform.eulerAngles.y;
        agent.transform.rotation = Quaternion.Euler(0, yRotation, 0);

    }
}
