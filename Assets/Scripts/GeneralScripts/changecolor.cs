using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;

public class changecolor : MonoBehaviour
{
    private bool is_night = false;

    public TextMeshProUGUI Amcur;
    public TextMeshProUGUI Amtot;

    public TextMeshProUGUI Antitot;
    public TextMeshProUGUI Antianti;

    public TextMeshProUGUI Weppri;
    public TextMeshProUGUI Wepsec;

    public TextMeshProUGUI mission;
    public TextMeshProUGUI sta;
    public TextMeshProUGUI rld;
    public TextMeshProUGUI aim;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (DayNightCycle.Instance != null)
        {
            is_night = DayNightCycle.Instance.IsNight();
        }
        if (is_night)
        {
            Amcur.color = Color.white;
            Amtot.color = Color.white;
            Antianti.color = Color.white;
            Antitot.color = Color.white;
            Weppri.color = Color.white;
            Wepsec.color = Color.white;
            mission.color = Color.white;
            sta.color = Color.white;
            rld.color = Color.white;
            aim.color = Color.white;
        }
        else
        {
            Amtot.color = Color.black;
            Antitot.color = Color.black;
            Weppri.color = Color.black;
            Wepsec.color = Color.black;
            mission.color = Color.black;
            sta.color = Color.black;
            Amcur.color = Color.black;
            Antianti.color = Color.black;
            rld.color = Color.black;
            aim.color = Color.black;
        }
    }
}
