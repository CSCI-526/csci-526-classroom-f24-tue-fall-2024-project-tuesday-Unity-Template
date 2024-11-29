using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerResource : MonoBehaviour
{
    public static PlayerResource Instance { get; set; }
    private static int currency = 100;
    private static int Pis_ammo = 14;
    private static int Rif_ammo = 60;
    private static int Anti = 6;

    public void ResteData()
    {
        currency = 100;
        Pis_ammo = 14;
        Rif_ammo = 60;
        Anti = 6;
    }
    public int GetPlayermonney()
    {
        return currency;
    }

    public int GetPlayerRif_ammo()
    {
        return Rif_ammo;
    }

    public int GetPlayerPis_ammo()
    {
        return Pis_ammo;
    }

    public int GetPlayeranti()
    {
        return Anti;
    }

    public bool Dec_money(int num, int mode)
    {
        currency -= num;
        if(mode == 0)
        {
            if (currency < 0)
            {
                currency += num;
                return false;
            }
            return true;
        }
        if(mode == 1)
        {
            if (currency < 0)
            {
                currency = 0;
                return false;
            }
            return true;
        }
        return true;
    }

    public bool inc_money(int num)
    {
        currency += num;
        if (currency > 999999999)
        {
            currency -= num;
            return false;
        }
        return true;
    }

    public void Dec_Rif(int num)
    {
        Rif_ammo -= num;
    }

    public void Dec_Pis(int num)
    {
        Pis_ammo -= num;
    }

    public void Inc_Rif(int num)
    {
        Rif_ammo += num;
    }

    public void Inc_Pis(int num)
    {
        Pis_ammo += num;
    }

    public void Dec_Ant(int num)
    {
        Anti -= num;
    }

    public void Inc_Ant(int num)
    {
        Anti += num;
    }

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
