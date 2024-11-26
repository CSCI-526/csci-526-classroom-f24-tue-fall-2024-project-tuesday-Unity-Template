

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class SpecialAgent : MonoBehaviour
{
    public float health;
    public float Maxhealth;
    public GameObject healthbar;
    public Slider slider;

    public Transform target;
    public int attackDamage = 10;
    public float attackInterval = 2f;

    //public float dayAttackRange = 2f;//day attck range
    //public float nightAttackRange = 10f; // night attack range
    //private float attackRange; // store dayattckrange or nightattack range

    private float attackRange = 2f;
    private float attackTimer;

    private NavMeshAgent navMeshAgent;

    private DayNightCycle dayNightCycle; //  DayNightCycle
    void Start()
    {
        health = Maxhealth;
        slider.value = calHealth();
        attackTimer = attackInterval;

        navMeshAgent = GetComponent<NavMeshAgent>();

        // check DayNightCycle
        dayNightCycle = FindObjectOfType<DayNightCycle>();


        if (target != null && navMeshAgent != null)
        {
            navMeshAgent.SetDestination(target.position);
        }
        else
        {
            Debug.LogWarning("SpecialAgent: Target or NavMeshAgent is not set correctly.");
        }
    }

    void Update()
    {
        HealthCheck();


        //// day or night to choose attackrange
        //if (dayNightCycle != null && dayNightCycle.IsNight())
        //{
        //    attackRange = nightAttackRange;
        //}
        //else
        //{
        //    attackRange = dayAttackRange;
        //}

        //navMeshAgent.stoppingDistance = attackRange; // stop update distance form navmesh



        attackTimer += Time.deltaTime;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        slider.value = calHealth();
    }

    private float calHealth()
    {
        return health / Maxhealth;
    }

    private void HealthCheck()
    {
        if (health <= 0)
        {
            DestroyAgent();
        }

        if (health > Maxhealth)
        {
            health = Maxhealth;
        }
        slider.value = calHealth();
    }



    public void DestroyAgent()
    {
        if (healthbar != null)
        {
            Destroy(healthbar);
        }
        Destroy(gameObject);
    }
}

