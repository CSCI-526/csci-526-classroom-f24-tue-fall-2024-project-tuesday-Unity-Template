using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class playerState : MonoBehaviour
{
    // Health control
    public float health;
    public float Maxhealth;
    public Slider slider;

    public GameObject lossUI;
    
    public float dealy = 1.5f;
    public float delay2 = 1f;
    private float timmer = 0;
    private float timmer2 = 0;

    void Start()
    {

        // Health ini
        health = Maxhealth;
        slider.value = calHealth();
    }

    public void TakeDamage()
    {
        if (DayNightCycle.Instance != null && DayNightCycle.Instance.IsNight())
        {
            health -= 20;
            return;
        }
        health -= 10;
        return;
    }

    private float calHealth()
    {
        return health / Maxhealth;
    }

    private void HealthCheck()
    {


        if (health <= 0)
        {
            lossUI.GetComponent<rangeUIControl>().showLoseUI();
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
        timmer2 += Time.deltaTime;
        timmer += Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            if(timmer2 > delay2)
            {
                TakeDamage();
                timmer2 = 0;
                timmer = 0;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            if (timmer >= dealy)
            {
                TakeDamage();
                timmer = 0;
            }
            timmer2 = 0;
        }
    }
}
