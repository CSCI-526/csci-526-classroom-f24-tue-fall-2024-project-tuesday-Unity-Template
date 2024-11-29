using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class curedCount : MonoBehaviour
{
    public static curedCount Instance { get; set; }

    public int level_mission;

    public int count = 0;
    public int killed = 0;

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
}
