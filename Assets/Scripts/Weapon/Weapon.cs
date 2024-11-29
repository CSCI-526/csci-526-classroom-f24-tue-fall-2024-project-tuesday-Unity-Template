using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public bool isActiveWeapon;
    // Shooting
    public bool isShooting, readyToShoot;
    bool allowResrt = true;
    public float shootingDelay;

    // Burst
    public int bulletPerBurst = 3;
    public int BurstBulletLeft;

    // Spread
    public float spreadIntensity;

    // Bullet Property
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public float bulletVelocity = 30;
    public float bulletPrefabLifeTime = 2f;

    // Shooting mode
    public ShootingMode currentMode;

    // Reloading
    public float reloadTime;
    public int magazineSize, bulletLeft;
    public bool isReloading;

    // Weapon Model
    public WeaponModel thisweapon;

    public Vector3 spawnPosition;
    public Vector3 spawnRotation;
    public int Weapondamage;

    public enum WeaponModel
    {
        Pistol,
        Rifel
    }

    public enum ShootingMode
    {
        Single,
        Burst,
        Auto
    }

    private void Awake()
    {
        readyToShoot = true;
        BurstBulletLeft = bulletPerBurst;
        bulletLeft = magazineSize;
    }

    private IEnumerator DestoryBulletAfterTime(GameObject bullet, float bulletPrefabLifeTime)
    {
        yield return new WaitForSeconds(bulletPrefabLifeTime);
        Destroy(bullet);
    }

    private void ResetShot()
    {
        readyToShoot = true;
        allowResrt = true;
    }

    private Vector3 GetshootingDirection()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

        RaycastHit hit;

        Vector3 targetPoint;
        if(Physics.Raycast(ray, out hit))
        {
            targetPoint = hit.point;
        }
        else
        {
            targetPoint= ray.GetPoint(100);
        }

        Vector3 direction = targetPoint - bulletSpawn.position;

        float x = UnityEngine.Random.Range(-spreadIntensity, spreadIntensity);
        float y = UnityEngine.Random.Range(-spreadIntensity, spreadIntensity);

        return direction + new Vector3(x, y, 0);
    }

    private void ReloadCompleted()
    {
        int need_amount = magazineSize - bulletLeft;
        int reload_amount = WeaponManager.Instance.DecreaseAmmo(need_amount, thisweapon);
        bulletLeft += reload_amount;
        isReloading = false;
    }

    private void Reload()
    {
        isReloading = true;
        Invoke("ReloadCompleted", reloadTime);
    }

    private void FireWeapon()
    {
        bulletLeft--;

        readyToShoot = false;

        Vector3 shootingDirection = GetshootingDirection().normalized;

        // Instantiate the bullet
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.identity);

        // Set damage
        bullet.GetComponent<Bullet>().bulletDamage = Weapondamage;

        // Change bullet to correct direction
        bullet.transform.forward = shootingDirection;

        // Fire the bullet
        bullet.GetComponent<Rigidbody>().AddForce(shootingDirection * bulletVelocity, ForceMode.Impulse);

        // Destory the bullet
        StartCoroutine(DestoryBulletAfterTime(bullet, bulletPrefabLifeTime));

        // Check if down shooting
        if (allowResrt)
        {
            Invoke("ResetShot", shootingDelay);
            allowResrt = false;
        }

        if(currentMode == ShootingMode.Burst && BurstBulletLeft > 1)
        {
            BurstBulletLeft--;
            Invoke("FireWeapon", shootingDelay);

        }
    }

    void Update()
    {

        if (isActiveWeapon)
        {
            if (currentMode == ShootingMode.Auto)
            {
                isShooting = Input.GetKey(KeyCode.Mouse0);
            }
            else if (currentMode == ShootingMode.Single || currentMode == ShootingMode.Burst)
            {
                isShooting = Input.GetKeyDown(KeyCode.Mouse0);
            }

            if (Input.GetKeyDown(KeyCode.R) && bulletLeft < magazineSize && isReloading == false && WeaponManager.Instance.CheckAmmoleft(thisweapon) > 0)
            {
                Reload();
            }

            if (readyToShoot && isShooting && bulletLeft > 0 && isReloading == false)
            {
                BurstBulletLeft = bulletPerBurst;
                FireWeapon();
            }

        }
    }

    
}
