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
    private float outBounddamage = 100f;

    public GameObject gameOverUI; // 拖入 GameOver UI
    public float maxZCoordinate = 24f; // 超过此 Z 坐标时触发 GameOver


    

    void Start()
    {

        // Health ini
        health = Maxhealth;
        slider.value = calHealth();
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
        if (transform.position.z > maxZCoordinate)
        {
            health -= outBounddamage;
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Enemy"))
        {

            if (timmer2 > delay2)
            {
                if (other.GetComponent<BigBoss>() != null)
                {
                    TakeDamage(20);
                    timmer2 = 0;
                    timmer = 0;
                }
                else { 
                   TakeDamage(10);
                   timmer2 = 0;
                   timmer = 0;
                }
                
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            if (timmer >= dealy)
            {
                if (other.GetComponent<BigBoss>() != null)
                {
                    TakeDamage(20);
                    timmer = 0;

                }
                else 
                { 
                    TakeDamage(10);
                    timmer = 0; 
                }
               
            }
            timmer2 = 0;
        }
    }
   
}
