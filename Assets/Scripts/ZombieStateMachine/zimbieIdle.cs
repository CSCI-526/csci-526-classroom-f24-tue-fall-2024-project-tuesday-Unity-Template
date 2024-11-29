using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zimbieIdle : StateMachineBehaviour
{
    float timer;
    public float idelTime = 0f;

    Transform player;
    ZombieState zstate;

    public float detectionAreaRadius = 10f;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer = 0;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }


    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        zstate = animator.GetComponent<ZombieState>();

        if (zstate.is_night)
        {
            detectionAreaRadius = 15f;
        }
        timer += Time.deltaTime;
        if(timer > idelTime)
        {
            animator.SetBool("isPatroling", true);
        }

        float distanceFromPlayer = Vector3.Distance(player.position, animator.transform.position);
        if(distanceFromPlayer < detectionAreaRadius)
        {
            animator.SetBool("isChasing", true);
        }
    }

}
