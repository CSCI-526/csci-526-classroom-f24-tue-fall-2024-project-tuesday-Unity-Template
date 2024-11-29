using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammobox : MonoBehaviour
{
    public int ammoAmount = 20;
    public AmmoType ammotype;

    public enum AmmoType
    {
        RifleAmmo,
        PistolAmmo
    }
}
