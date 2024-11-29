using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    public float destroyAfterSeconds = 30f; 

    void Start()
    {
        Destroy(gameObject, destroyAfterSeconds);
    }
}
