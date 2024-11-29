using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    public static HUDManager Instance { get; set; }

    //UI
    public TextMeshProUGUI currentAmmo;
    public TextMeshProUGUI totalAmmo;

    public Image activeWeapon;
    public Image secondWeapon;

    public Image antiPic;
    public TextMeshProUGUI antiNum;
    public TextMeshProUGUI PrimName;
    public TextMeshProUGUI SendName;
    public TextMeshProUGUI MissionBoard;
    public TextMeshProUGUI statistic;

    public Sprite emptyslot;

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

    private void Update()
    {
        if(curedCount.Instance.count < curedCount.Instance.level_mission)
        {
            MissionBoard.text = $"{"Mission: Cure at least " + curedCount.Instance.level_mission + " Zombies "}";
        }
        else if(curedCount.Instance.count >= curedCount.Instance.level_mission) 
        {
            MissionBoard.text = $"{"Mission accomplish, Leave the area."}";
        }
        statistic.text = $"{"Cured: " + curedCount.Instance.count + "\n" + "Killed: " + curedCount.Instance.killed }";
        antiNum.text = $"{WeaponManager.Instance.totalAntis}";
        Weapon actived = WeaponManager.Instance.activeWeaponSlot.GetComponentInChildren<Weapon>();
        Weapon unacted = GetUnActivedSlot().GetComponentInChildren<Weapon>();

        if (actived)
        {
            currentAmmo.text = $"{actived.bulletLeft / actived.bulletPerBurst}";
            totalAmmo.text = $"{"/" + WeaponManager.Instance.CheckAmmoleft(actived.thisweapon)}";

            Weapon.WeaponModel model = actived.thisweapon;

            if(model == Weapon.WeaponModel.Pistol)
            {
                PrimName.text = $"Pistol";
            }
            if (model == Weapon.WeaponModel.Rifel)
            {
                PrimName.text = $"Rifle";
            }

            if (unacted)
            {
                if (unacted.thisweapon == Weapon.WeaponModel.Pistol)
                {
                    SendName.text = $"Pistol";
                }
                if (unacted.thisweapon == Weapon.WeaponModel.Rifel)
                {
                    SendName.text = $"Rifle";
                }
            }
        }
        else
        {
            currentAmmo.text = "";
            totalAmmo.text = "";

            PrimName.text = "";
            SendName.text = "";
        }
    }

    private GameObject GetUnActivedSlot()
    {
        foreach(GameObject slots in WeaponManager.Instance.weaponSlots)
        {
            if(slots != WeaponManager.Instance.activeWeaponSlot)
            {
                return slots;
            }
        }
        return null;
    }
}
