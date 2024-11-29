using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class breakablewall : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void onCollisionEnter2D(Collision2D other)
    {
        Debug.Log("Breakable Wall Collided with " + other.gameObject.name);
        if (other.gameObject.name == "Bullet (Clone)")
        {
            Destroy(gameObject);
        }
    }
}
