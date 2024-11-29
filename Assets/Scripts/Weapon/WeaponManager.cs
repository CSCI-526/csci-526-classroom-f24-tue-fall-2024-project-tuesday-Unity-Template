using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Weapon;

public class WeaponManager : MonoBehaviour
{
    public static WeaponManager Instance { get; set; }

    public List<GameObject> weaponSlots;

    public GameObject activeWeaponSlot;

    public int totalRifelAmmo = 0;
    public int totalPistolAmmo = 0;
    public int totalAntis = 0;

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

    private void Start()
    {
        activeWeaponSlot = weaponSlots[0];
    }
    private void Update()
    {
        foreach (GameObject weaponSlot in weaponSlots)
        {
            if (weaponSlot == activeWeaponSlot)
            {
                weaponSlot.SetActive(true);
                if (activeWeaponSlot.transform.childCount > 0)
                {
                    activeWeaponSlot.transform.GetChild(0).gameObject.GetComponent<Weapon>().isActiveWeapon = true;
                }
            }
            else
            {
                weaponSlot.SetActive(false);
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwitchActiveSlot(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SwitchActiveSlot(1);
        }
    }

    public void PickupWeapon(GameObject pickedupWeapon)
    {
        ADDWeaponIntoActiveSlot(pickedupWeapon);
    }

    private void DropCurrentweapon(GameObject pickedupWeapon)
    {
        if(activeWeaponSlot.transform.childCount > 0)
        {
            var weaponToDrop = activeWeaponSlot.transform.GetChild(0).gameObject;

            weaponToDrop.GetComponent<Weapon>().isActiveWeapon = false;

            weaponToDrop.transform.SetParent(pickedupWeapon.transform.parent);
            weaponToDrop.transform.localPosition = pickedupWeapon.transform.localPosition;
            weaponToDrop.transform.localRotation = pickedupWeapon.transform.localRotation;
        }
    }

    private void ADDWeaponIntoActiveSlot(GameObject pickedupWeapon)
    {
        DropCurrentweapon(pickedupWeapon);

        pickedupWeapon.transform.SetParent(activeWeaponSlot.transform, false);

        Weapon weapon = pickedupWeapon.GetComponent<Weapon>();

        pickedupWeapon.transform.localPosition = new Vector3(weapon.spawnPosition.x, weapon.spawnPosition.y, weapon.spawnPosition.z);
        pickedupWeapon.transform.localRotation = Quaternion.Euler(weapon.spawnRotation.x, weapon.spawnRotation.y, weapon.spawnRotation.z);

        weapon.isActiveWeapon = true;
    }

    public void SwitchActiveSlot(int slotnum)
    {
        if(activeWeaponSlot.transform.childCount > 0)
        {
            Weapon currentweapon = activeWeaponSlot.transform.GetChild(0).GetComponent<Weapon>();
            currentweapon.isActiveWeapon = false;
        }

        activeWeaponSlot = weaponSlots[slotnum];

        if(activeWeaponSlot.transform.childCount > 0)
        {
            Weapon newWeapon = activeWeaponSlot.transform.GetChild(0).GetComponent<Weapon>();
            newWeapon.isActiveWeapon = true;
        }
    }

    internal void PickupAmmo(Ammobox name)
    {
        switch (name.ammotype)
        {
            case Ammobox.AmmoType.PistolAmmo:
                totalPistolAmmo += name.ammoAmount;
                break;
            case Ammobox.AmmoType.RifleAmmo:
                totalRifelAmmo += name.ammoAmount;
                break;
        }
    }

    internal int DecreaseAmmo(int bulletodecrease, Weapon.WeaponModel thisweapon)
    {
        if(thisweapon == WeaponModel.Rifel)
        {
            if(totalRifelAmmo >= bulletodecrease)
            {
                totalRifelAmmo -= bulletodecrease;
                return bulletodecrease;
            }
            else
            {
                int actual = totalRifelAmmo;
                totalRifelAmmo = 0;
                return actual;
            }
        }
        else if(thisweapon == WeaponModel.Pistol)
        {
            if (totalPistolAmmo >= bulletodecrease)
            {
                totalPistolAmmo -= bulletodecrease;
                return bulletodecrease;
            }
            else
            {
                int actual = totalPistolAmmo;
                totalPistolAmmo = 0;
                return actual;
            }
        }
        else
        {
            Debug.Log("Type Error");
            return 0;
        }
    }

    public int CheckAmmoleft(Weapon.WeaponModel thisweapon)
    {
        switch (thisweapon)
        {
            case Weapon.WeaponModel.Rifel:
                return totalRifelAmmo;
            case Weapon.WeaponModel.Pistol:
                return totalPistolAmmo;
            default:
                return 0;
        }
    }

    public bool UseAnti()
    {
        if(totalAntis > 0)
        {
            totalAntis--;
            return true;
        }
        return false;
    }
}
