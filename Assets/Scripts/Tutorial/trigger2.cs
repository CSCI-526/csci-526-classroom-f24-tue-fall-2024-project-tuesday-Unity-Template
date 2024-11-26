using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trigger2 : MonoBehaviour
{
    public GameObject gate2;
    public GameObject curetext;
    public GameObject prevtext;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(prevtext != null)
            {
                Destroy(prevtext);
            }
            gate2.SetActive(true);
            curetext.SetActive(true);
            Destroy(gameObject);
        }
    }
}
