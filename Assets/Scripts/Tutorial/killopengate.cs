using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class killopengate : MonoBehaviour
{
    public GameObject zombie;

    // Update is called once per frame
    void Update()
    {
        if(zombie != null && zombie.GetComponent<ZombieState>().health <= 1)
        {
            gameObject.SetActive(false);
        }
    }
}
