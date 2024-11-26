using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trigger3 : MonoBehaviour
{
    public GameObject gate3;
    public GameObject infec;
    public GameObject prevtext;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (prevtext != null)
            {
                Destroy(prevtext);
            }
            gate3.SetActive(true);
            infec.SetActive(true);
            Destroy(gameObject);
        }
    }
}
