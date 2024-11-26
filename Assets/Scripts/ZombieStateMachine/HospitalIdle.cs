using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HospitalIdle : StateMachineBehaviour
{
    float timer;
    public float idelTime = 0f;

    Transform player;

    public float detectionAreaRadius = 10f;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer = 0;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer += Time.deltaTime;
        if (timer > idelTime)
        {
            animator.SetBool("isPatroling", true);
        }

        Zombie zombieComponent = animator.GetComponent<Zombie>();
        float distanceFromPlayer = Vector3.Distance(player.position, animator.transform.position);
        if ( zombieComponent != null && zombieComponent.isIlluminated)
        {
            animator.SetBool("isChasing", true);
        }
    }
}