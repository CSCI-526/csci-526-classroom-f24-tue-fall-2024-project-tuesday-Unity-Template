using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ZombieState : MonoBehaviour
{
    // Health control
    public float health;
    public float Maxhealth;
    public GameObject healthbar; 
    public Slider slider; 

    public bool is_night = false;

    public Light flashlight; 
    public float detectionDistance = 25f; 

    private float timmer = 30f;
    private Renderer objectRenderer;
    public int zombie_state = 0;
    private Color[] colors = { new Color(1f, 0.5f, 0f), Color.red, new Color(0.5f, 0f, 0.5f) };

    // Movement Control
    private Animator animator;
    private NavMeshAgent navMeshAgent;

    void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        objectRenderer.material.color = colors[zombie_state];

        health = Maxhealth;
        slider.value = calHealth();

        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();

        if (SceneManager.GetActiveScene().name == "Level3")
        {
            if (healthbar != null)
            {
                healthbar.SetActive(false); 
            }
        }
        else
        {
            if (healthbar != null)
            {
                healthbar.SetActive(true);
            }
        }
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
        else if (zombie_state == 0)
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
        if (DayNightCycle.Instance != null)
        {
            is_night = DayNightCycle.Instance.IsNight();
        }

        CheckFlashlight(); 
    }

    private void CheckFlashlight()
    {
        if (SceneManager.GetActiveScene().name == "Level3")
        {
            if (flashlight != null && flashlight.enabled)
            {
                Vector3 directionToZombie = (transform.position - flashlight.transform.position).normalized;
                Ray ray = new Ray(flashlight.transform.position, directionToZombie);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, detectionDistance))
                {
                    if (hit.collider.gameObject == gameObject)
                    {
                        healthbar.SetActive(true);
                        return;
                    }
                }
            }
            healthbar.SetActive(false); 
        }
        else
        {
            if (healthbar != null)
            {
                healthbar.SetActive(true);
            }
        }
    }
}
