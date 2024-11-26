using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class box_assist : MonoBehaviour
{
    public bool rbox = false;
    public bool pbox = false;
    public bool abox = false;

    public TextMeshProUGUI tex;
    public static box_assist Instance { get; set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (rbox == true && pbox == false && abox == false)
        {
            tex.text = "Press 'E' to get ammo";
        }
        else if (rbox == false && pbox == true && abox == false)
        {
            tex.text = "Press 'E' to get ammo";
        }
        else if(rbox == false && pbox == false && abox == true)
        {
            tex.text = "Press 'E' to get antidote";
        }
        else
        {
            tex.text = "";
        }
        rbox = false;
        pbox = false;
        abox = false;
    }
}
