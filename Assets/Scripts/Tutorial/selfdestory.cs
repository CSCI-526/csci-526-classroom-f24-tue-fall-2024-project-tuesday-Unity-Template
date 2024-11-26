using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class selfdestory : MonoBehaviour
{
    public GameObject zombie;

    // Update is called once per frame
    void Update()
    {
        if (zombie == null)
        {
            Destroy(gameObject);
        }
    }
}
