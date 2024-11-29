using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Reloading : MonoBehaviour
{
    public TextMeshProUGUI Reload;
    private Weapon weapon_script;
    void Start()
    {
        weapon_script = GetComponent<Weapon>();
    }

    // Update is called once per frame
    void Update()
    {
        if (weapon_script.isReloading)
        {
            Reload.text = $"{"Reloading..."}";
        }
        else
        {
            Reload.text = "";
        }
    }
}
