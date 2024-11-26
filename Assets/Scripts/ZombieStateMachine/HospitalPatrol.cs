using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HospitalPatrol : StateMachineBehaviour
{
    float timer;
    public float patrolingTime = 10f;

    Transform player;
    NavMeshAgent agent;

    //public float detectionArea = 10f;
    public float patrolSpeed = 2.0f;

    // day and night detect
    DayNightCycle dayNightCycle;
    public float dayDetectionArea = 10f;
    public float nightDetectionArea = 20f;
    private float detectionArea;



    List<Transform> waypointList = new List<Transform>();

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = animator.GetComponent<NavMeshAgent>();

        //dayand night
        dayNightCycle = FindObjectOfType<DayNightCycle>();

        agent.speed = patrolSpeed;
        timer = 0;

        GameObject waypoint = animator.GetComponent<zombie_nav>().nvob;
        foreach (Transform t in waypoint.transform)
        {
            waypointList.Add(t);
        }

        Vector3 nectPosition = waypointList[Random.Range(0, waypointList.Count)].position;
        agent.SetDestination(nectPosition);
    }


    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            agent.SetDestination(waypointList[Random.Range(0, waypointList.Count)].position);
        }

        // To idle
        timer += Time.deltaTime;
        if (timer > patrolingTime)
        {
            animator.SetBool("isPatroling", false);
        }

        // To Chase
        Zombie zombieComponent = animator.GetComponent<Zombie>();


        //day and night switch
        if (dayNightCycle != null && dayNightCycle.IsNight())
        {
            detectionArea = nightDetectionArea;
        }
        else
        {
            detectionArea = dayDetectionArea;
        }


        float distanceFromPlayer = Vector3.Distance(player.position, animator.transform.position);
        if (zombieComponent != null && zombieComponent.isIlluminated)
        {
            animator.SetBool("isChasing", true);
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent.SetDestination(agent.transform.position);
    }
}
