using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int bulletDamage;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Target"))
        {
            Destroy(gameObject);
        }

        

       
        if (collision.gameObject.CompareTag("Enemy"))
        {           
            SpecialAgent specialAgent = collision.gameObject.GetComponent<SpecialAgent>();
 if (specialAgent != null)
        {
            specialAgent.TakeDamage(bulletDamage);
        }
  collision.gameObject.GetComponent<ZombieState>().TakeDamage(bulletDamage);

            Destroy(gameObject);
        }
          
           
       

        if (collision.gameObject.CompareTag("Other"))
        {
            collision.gameObject.GetComponent<ZombieState>().TakeDamage(bulletDamage);
            Destroy(gameObject);
        }
    }
}
