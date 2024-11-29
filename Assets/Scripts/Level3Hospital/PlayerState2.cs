using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerState2 : MonoBehaviour
{
    // Health control
    public float health;
    public float Maxhealth;
    public Slider slider;

    public GameObject lossUI;

    public float delay = 1.5f;
    public float delay2 = 1f;
    private float timer = 0;
    private float timer2 = 0;

    void Start()
    {
        health = Maxhealth;
        slider.value = CalculateHealth();
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
    }

    private float CalculateHealth()
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
        slider.value = CalculateHealth();
    }

    void Update()
    {
        HealthCheck();
        timer2 += Time.deltaTime;
        timer += Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            if (timer2 > delay2)
            {TakeDamage(10);
                
                timer2 = 0;
                timer = 0;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            if (timer >= delay)
            {
               
               
                    TakeDamage(10);
                
                timer = 0;
            }
            timer2 = 0;
        }
    }
}
