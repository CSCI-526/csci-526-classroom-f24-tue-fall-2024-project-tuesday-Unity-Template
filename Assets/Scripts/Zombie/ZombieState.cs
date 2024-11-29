using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class ZombieState : MonoBehaviour
{
    // Health control
    public float health;
    public float Maxhealth;
    public GameObject healthbar;
    public Slider slider;

    // Color Controll
    private float timmer = 30f;
    private Renderer objectRenderer;
    public int zombie_state = 0;
    private Color[] colors = { Color.green, new Color(1f, 0.5f, 0f), Color.red };

    // Movement Control
    private Animator animator;
    private NavMeshAgent navMeshAgent;

    void Start()
    {
        // Color ini
        objectRenderer = GetComponent<Renderer>();
        objectRenderer.material.color = colors[zombie_state];

        // Health ini
        health = Maxhealth;
        slider.value = calHealth();

        // State ini
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void colordeeper()
    {
        if (objectRenderer != null && zombie_state < 2)
        {
            zombie_state++;
            objectRenderer.material.color = colors[zombie_state];
            timmer = 10f;
        }
    }

    public void colorlighter()
    {
        if (objectRenderer != null && zombie_state > 0)
        {
            zombie_state--;
            objectRenderer.material.color = colors[zombie_state];
            timmer = 10f;
            health = Maxhealth;
        }
        else if(zombie_state == 0)
        {
            curedCount.Instance.count++;
            Destroy(gameObject);
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
    }

    private float calHealth()
    {
        return health / Maxhealth;
    }

    private void HealthCheck()
    {
        timmer -= Time.deltaTime;
        if (timmer <= 0)
        {
            colordeeper();
        }

  
        if (health <= 0)
        {
            curedCount.Instance.killed++;
            Destroy(gameObject);
        }
        if (health > Maxhealth)
        {
            health = Maxhealth;
        }
        slider.value = calHealth();
    }

    void Update()
    {
        HealthCheck();
    }
}
