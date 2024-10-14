using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class icewall : MonoBehaviour
{
    private int hitCount = 0; 
    public int requiredHits = 3; 
    private void onCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.name == "Bullet (Clone)")
        {
            RegisterHit(); 
        }
    }

    public void RegisterHit(int hitNum = 1)
        {
            hitCount += hitNum; 
            Debug.Log("Ice Wall hit count: " + hitCount);
            if (hitCount >= requiredHits)
            {
                Destroy(gameObject);
                Debug.Log("Ice Wall destroyed after 3 hits.");
            }
        }
}
