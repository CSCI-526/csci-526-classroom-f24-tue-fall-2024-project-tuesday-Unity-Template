using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    public static InteractionManager Instance { get; set; }

    public Weapon hoveredWeapon = null;
    public Ammobox hoveredAmmobox = null;

    public float rayLength = 2f;

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
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, rayLength))
        {
            GameObject objectHitByRaycast = hit.transform.gameObject;

            if (objectHitByRaycast.GetComponent<Weapon>() && objectHitByRaycast.GetComponent<Weapon>().isActiveWeapon == false)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    WeaponManager.Instance.PickupWeapon(objectHitByRaycast.gameObject); 
                }
            }


            // Ammobox
            if (objectHitByRaycast.GetComponent<Ammobox>())
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    hoveredAmmobox = objectHitByRaycast.gameObject.GetComponent<Ammobox>();
                    WeaponManager.Instance.PickupAmmo(hoveredAmmobox);
                    Destroy(objectHitByRaycast.gameObject);
                }
            }

            if (objectHitByRaycast.CompareTag("Enemy") || objectHitByRaycast.CompareTag("Other"))
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    if(objectHitByRaycast.GetComponent<ZombieState>().health <= 50 && WeaponManager.Instance.UseAnti())
                    {
                        objectHitByRaycast.GetComponent<ZombieState>().colorlighter();
                    }
                    if (objectHitByRaycast.GetComponent<ZombieState>().health > 50 && WeaponManager.Instance.UseAnti())
                    {
                        
                    }
                }
            }

        }
    }
}
