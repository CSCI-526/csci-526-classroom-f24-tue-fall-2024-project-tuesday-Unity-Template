/*原有
 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class WallHealth : MonoBehaviour
{
    public float health = 100f;
    public float Maxhealth = 100f;
    public Slider slider;
    public GameObject healthbar;
    public GameObject nextWall; // 要激活吸引行为的下一个墙体


    void Start()
    {
       
        health = Maxhealth;
        slider.value = calHealth();

    }
    void Update()
    {
        HealthCheck();

        
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
            Destroy(gameObject);
        }
        if (health > Maxhealth)
        {
            health = Maxhealth;
        }
        slider.value = calHealth();
    }
    private void OnDestroy()
    {
        // 当墙体被销毁时，通知下一个墙体激活吸引行为
        if (nextWall != null)
        {
            WallAttraction wallAttraction = nextWall.GetComponent<WallAttraction>();
            if (wallAttraction != null)
            {
                wallAttraction.ActivateAttraction();
            }
        }
    }
}*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WallHealth : MonoBehaviour
{
    public float health = 100f;
    public float Maxhealth = 100f;
    public Slider slider;
    public GameObject healthbar;
    public GameObject nextWall; // Next wall to activate attraction behavior
    public GameObject explosionEffect; // Reference to VFX_EasyExplosion prefab
    public SpecialAgent specialAgent; // Reference to the SpecialAgent

    void Start()
    {
        health = Maxhealth;
        slider.value = calHealth();
    }

    void Update()
    {
        HealthCheck();
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
            // Trigger explosion effect
            if (explosionEffect != null)
            {
                Instantiate(explosionEffect, transform.position, Quaternion.identity);
            }

            // Set SpecialAgent health to zero and destroy it
            if (specialAgent != null)
            {
                specialAgent.TakeDamage((int)specialAgent.health); // Reduce SpecialAgent health to zero
                specialAgent.DestroyAgent(); // Destroy the agent
            }

            Destroy(gameObject); // Destroy the wall
        }

        if (health > Maxhealth)
        {
            health = Maxhealth;
        }

        slider.value = calHealth();
    }

    private void OnDestroy()
    {
        // When the wall is destroyed, notify the next wall to activate attraction
        if (nextWall != null)
        {
            WallAttraction wallAttraction = nextWall.GetComponent<WallAttraction>();
            if (wallAttraction != null)
            {
                wallAttraction.ActivateAttraction();
            }
        }
    }
}
