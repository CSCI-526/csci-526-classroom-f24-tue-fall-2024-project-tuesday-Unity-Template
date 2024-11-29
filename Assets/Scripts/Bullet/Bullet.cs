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

        if (collision.gameObject.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Breakable wall"))
        {
            WallHealth wallHealth = collision.gameObject.GetComponent<WallHealth>();
            if (wallHealth != null)
            {
                wallHealth.TakeDamage(bulletDamage);
            }
            Destroy(gameObject);
        }

        /* if (collision.gameObject.CompareTag("Enemy"))
         {
             collision.gameObject.GetComponent<ZombieState>().TakeDamage(bulletDamage);
 ;
             Destroy(gameObject);
         }*/
        if (collision.gameObject.CompareTag("Enemy"))
        {
            ZombieState zombieState = collision.gameObject.GetComponent<ZombieState>();
            if (zombieState != null)
            {
                zombieState.TakeDamage(bulletDamage);
            }

            SpecialAgent specialAgent = collision.gameObject.GetComponent<SpecialAgent>();
            if (specialAgent != null)
            {
                specialAgent.TakeDamage(bulletDamage);
            }

            BigBoss bigBoss = collision.gameObject.GetComponent<BigBoss>();
            if (bigBoss != null)
            {
                bigBoss.TakeDamage(bulletDamage);
            }

            Destroy(gameObject);
        }


        if (collision.gameObject.CompareTag("Other"))
        {
            collision.gameObject.GetComponent<ZombieState>().TakeDamage(bulletDamage);
            Destroy(gameObject);
        }
    }
}
