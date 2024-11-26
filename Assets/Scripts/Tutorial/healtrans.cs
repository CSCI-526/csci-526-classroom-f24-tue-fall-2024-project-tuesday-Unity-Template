using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healtrans : MonoBehaviour
{
    public GameObject z1;
    public GameObject z2;
    public GameObject z3;

    public GameObject gate3;

    private void Update()
    {
        if(z1==null && z2==null && z3 == null)
        {
            gate3.SetActive(false);
            Destroy(gameObject);
        }
    }
}
