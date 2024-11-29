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

            if (objectHitByRaycast.CompareTag("Enemy") || objectHitByRaycast.CompareTag("Other"))
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    if(objectHitByRaycast.GetComponent<ZombieState>().health <= 50 && WeaponManager.Instance.UseAnti())
                    {
                        if(PlayerResource.Instance != null)
                        {
                            PlayerResource.Instance.Dec_Ant(1);
                        }
                        objectHitByRaycast.GetComponent<ZombieState>().colorlighter();
                    }
                    if (objectHitByRaycast.GetComponent<ZombieState>().health > 50 && WeaponManager.Instance.UseAnti())
                    {
                        if (PlayerResource.Instance != null)
                        {
                            PlayerResource.Instance.Dec_Ant(1);
                        }
                    }
                }
            }

            if (objectHitByRaycast.CompareTag("SPUI"))
            {
                if (Input.GetKeyDown(KeyCode.E) && SafeHouseUIControl.Instance.disE ==false)
                {
                    SafeHouseUIControl.Instance.showLevelSelection();
                }
                else if (Input.GetKeyDown(KeyCode.Mouse0) && SafeHouseUIControl.Instance.disE == false)
                {
                    SafeHouseUIControl.Instance.showLevelSelection();
                }
            }

            if (objectHitByRaycast.CompareTag("Rbox"))
            {
                if (Input.GetKeyDown(KeyCode.E) && PlayerResource.Instance != null)
                {
                    if (PlayerResource.Instance.Dec_money(30,0))
                    {
                        PlayerResource.Instance.Inc_Rif(30);
                    }
                }
                else if (Input.GetKeyDown(KeyCode.Mouse0) && PlayerResource.Instance != null)
                {
                    if (PlayerResource.Instance.Dec_money(30, 0))
                    {
                        PlayerResource.Instance.Inc_Rif(30);
                    }
                }
            }

            if (objectHitByRaycast.CompareTag("Pbox"))
            {
                if (Input.GetKeyDown(KeyCode.E) && PlayerResource.Instance != null)
                {
                    if (PlayerResource.Instance.Dec_money(20, 0))
                    {
                        PlayerResource.Instance.Inc_Pis(7);
                    }
                }
                else if (Input.GetKeyDown(KeyCode.Mouse0) && PlayerResource.Instance != null)
                {
                    if (PlayerResource.Instance.Dec_money(20, 0))
                    {
                        PlayerResource.Instance.Inc_Pis(7);
                    }
                }
            }

            if (objectHitByRaycast.CompareTag("Abox"))
            {
                if (Input.GetKeyDown(KeyCode.E) && PlayerResource.Instance != null)
                {
                    if (PlayerResource.Instance.Dec_money(10, 0))
                    {
                        PlayerResource.Instance.Inc_Ant(1);
                    }
                }
                else if (Input.GetKeyDown(KeyCode.Mouse0) && PlayerResource.Instance != null)
                {
                    if (PlayerResource.Instance.Dec_money(10, 0))
                    {
                        PlayerResource.Instance.Inc_Ant(1);
                    }
                }
            }

        }
    }
}
