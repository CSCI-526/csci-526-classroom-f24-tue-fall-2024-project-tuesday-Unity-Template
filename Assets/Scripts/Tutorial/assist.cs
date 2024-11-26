using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class assist : MonoBehaviour
{
    public bool show = false;
    public TextMeshProUGUI tex;

    // Update is called once per frame
    void Update()
    {
        if(show && GetComponent<ZombieState>().health <= 50)
        {
            tex.text = "Press 'E' to heal zombie";
        }
        else if (show && GetComponent<ZombieState>().health > 50)
        {
            tex.text = "Shoot zombie first";
        }
        else
        {
            tex.text = "";
        }
        show = false;
    }
}
