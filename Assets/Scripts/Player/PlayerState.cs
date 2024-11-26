using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

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

    private string stat;
    private string sceneName;
    private Datacollection datacollation;

    private bool datasent = false;

    void Start()
    {

        // Health ini
        datacollation = GetComponent<Datacollection>();
        
        health = Maxhealth;
        slider.value = calHealth();
    }

    public void TakeDamage(int dmg)
    {
        if(dmg != 0)
        {
            health -= dmg;
        }
        if (DayNightCycle.Instance != null && DayNightCycle.Instance.IsNight())
        {
            health -= 20;
            return;
        }
        health -= 10;
        return;
    }
    public void calHealthstat(){
        sceneName = SceneManager.GetActiveScene().name;
        float temp = health / Maxhealth;
        if(temp <= 0.3){
            stat = "below 30%";
        }
        else if((temp > 0.3) && (temp <= 0.5)){
            stat = "below 50%";
        }
        else if((temp > 0.5) && (temp <= 0.75)){
            stat = "below 75%";
        }
        else if((temp > 0.75) && (temp <= 1)){
            stat = "above 75%";
        }
        if(!datasent){
            Debug.Log("Calling Sendhealthstat for  event...");
            datacollation.SendHealthstat(sceneName + ": " + health/Maxhealth * 100 + "%");
            datasent = true;
        }
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
                if (other.GetComponent<ZombieState>() != null && other.GetComponent<ZombieState>().boss == true)
                {
                    TakeDamage(50);
                }
                else TakeDamage(0);
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
                if(other.GetComponent<ZombieState>() != null && other.GetComponent<ZombieState>().boss == true)
                {
                    TakeDamage(50);
                }
                else TakeDamage(0);
                timmer = 0;
            }
            timmer2 = 0;
        }
    }
}
