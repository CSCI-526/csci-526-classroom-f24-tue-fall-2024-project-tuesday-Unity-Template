using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieNavmesh : MonoBehaviour
{
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        if (agent != null)
        {
            agent.enabled = false;
            StartCoroutine(EnableNavMeshAfterDelay(30f)); 
        }
    }

    private IEnumerator EnableNavMeshAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (agent != null)
        {
            agent.enabled = true;
        }
    }
}
