using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trigger4 : MonoBehaviour
{
    public GameObject gate4;
    public GameObject prevtext;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (prevtext != null)
            {
                Destroy(prevtext);
            }
            gate4.SetActive(true);
            Destroy(gameObject);
        }
    }
}
