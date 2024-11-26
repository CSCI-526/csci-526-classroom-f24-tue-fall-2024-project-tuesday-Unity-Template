using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class trigger1 : MonoBehaviour
{
    public GameObject gate;
    public GameObject text;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gate.SetActive(true);
            text.SetActive(true);
            Destroy(gameObject);
        }
    }
}
