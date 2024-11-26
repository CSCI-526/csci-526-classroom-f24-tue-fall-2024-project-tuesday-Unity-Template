using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InteractionManager : MonoBehaviour
{
    public static InteractionManager Instance { get; set; }

    public Weapon hoveredWeapon = null;
    public Ammobox hoveredAmmobox = null;

    public float rayLength = 2f;
    private Datacollection datacollation;
    private bool datasent = false;
    private string sceneName;
    private int antido_use;
    public int count_cured;

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
                if(objectHitByRaycast.GetComponent<assist>() != null)
                {
                    objectHitByRaycast.GetComponent<assist>().show = true;
                }
                if (Input.GetKeyDown(KeyCode.E))
                {
                    antido_use++;
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
                    int temp = PlayerResource.Instance.GetPlayerRif_ammo();
                    if (temp < 999 && PlayerResource.Instance.Dec_money(30, 0))
                    {
                        PlayerResource.Instance.Inc_Rif(30);
                    }
                }
                else if (Input.GetKeyDown(KeyCode.Mouse0) && PlayerResource.Instance != null)
                {
                    int temp = PlayerResource.Instance.GetPlayerRif_ammo();
                    if (temp < 999 && PlayerResource.Instance.Dec_money(30, 0))
                    {
                        PlayerResource.Instance.Inc_Rif(30);
                    }
                }
            }

            if (objectHitByRaycast.CompareTag("Pbox"))
            {
                if (Input.GetKeyDown(KeyCode.E) && PlayerResource.Instance != null)
                {
                    int temp = PlayerResource.Instance.GetPlayerPis_ammo();
                    if (temp < 999 && PlayerResource.Instance.Dec_money(20, 0))
                    {
                        PlayerResource.Instance.Inc_Pis(7);
                    }
                }
                else if (Input.GetKeyDown(KeyCode.Mouse0) && PlayerResource.Instance != null)
                {
                    int temp = PlayerResource.Instance.GetPlayerPis_ammo();
                    if (temp < 999 && PlayerResource.Instance.Dec_money(20, 0))
                    {
                        PlayerResource.Instance.Inc_Pis(7);
                    }
                }
            }

            if (objectHitByRaycast.CompareTag("Abox"))
            {
                if (Input.GetKeyDown(KeyCode.E) && PlayerResource.Instance != null)
                {
                    int temp = PlayerResource.Instance.GetPlayeranti();
                    if (temp < 99 && PlayerResource.Instance.Dec_money(10, 0))
                    {
                        PlayerResource.Instance.Inc_Ant(1);
                    }
                }
                else if (Input.GetKeyDown(KeyCode.Mouse0) && PlayerResource.Instance != null)
                {
                    int temp = PlayerResource.Instance.GetPlayeranti();
                    if (temp < 99 && PlayerResource.Instance.Dec_money(10, 0))
                    {
                        PlayerResource.Instance.Inc_Ant(1);
                    }
                }
            }

            if (objectHitByRaycast.CompareTag("TRbox"))
            {
                if(box_assist.Instance != null)
                {
                    box_assist.Instance.rbox = true;
                }
                if (Input.GetKeyDown(KeyCode.E) && WeaponManager.Instance != null && WeaponManager.Instance.totalRifelAmmo <= 999)
                {
                    if(WeaponManager.Instance.totalRifelAmmo + 30 >= 999)
                    {
                        WeaponManager.Instance.totalRifelAmmo = 999;
                    }
                    else
                    {
                        WeaponManager.Instance.totalRifelAmmo += 30;
                    }
                }

            }

            if (objectHitByRaycast.CompareTag("TPbox"))
            {
                if (box_assist.Instance != null)
                {
                    box_assist.Instance.pbox = true;
                }
                if (Input.GetKeyDown(KeyCode.E) && WeaponManager.Instance != null && WeaponManager.Instance.totalPistolAmmo <= 999)
                {
                    if (WeaponManager.Instance.totalPistolAmmo + 7 >= 999)
                    {
                        WeaponManager.Instance.totalPistolAmmo = 999;
                    }
                    else
                    {
                        WeaponManager.Instance.totalPistolAmmo += 30;
                    }
                }

            }

            if (objectHitByRaycast.CompareTag("TAbox"))
            {
                if (box_assist.Instance != null)
                {
                    box_assist.Instance.abox = true;
                }
                if (Input.GetKeyDown(KeyCode.E) && WeaponManager.Instance != null && WeaponManager.Instance.totalAntis <= 99)
                {
                    if (WeaponManager.Instance.totalAntis + 1 >= 99)
                    {
                        WeaponManager.Instance.totalAntis = 99;
                    }
                    else
                    {
                        WeaponManager.Instance.totalAntis += 1;
                    }
                }

            }

            if (objectHitByRaycast.CompareTag("RRbox"))
            {
                if (Input.GetKeyDown(KeyCode.E) && PlayerResource.Instance != null)
                {
                    if (PlayerResource.Instance.recyclerif(30))
                    {
                        PlayerResource.Instance.inc_money(30);
                    }
                }
                else if (Input.GetKeyDown(KeyCode.Mouse0) && PlayerResource.Instance != null)
                {
                    if (PlayerResource.Instance.recyclerif(30))
                    {
                        PlayerResource.Instance.inc_money(30);
                    }
                }
            }

            if (objectHitByRaycast.CompareTag("RPbox"))
            {
                if (Input.GetKeyDown(KeyCode.E) && PlayerResource.Instance != null)
                {
                    if (PlayerResource.Instance.recyclepis(7))
                    {
                        PlayerResource.Instance.inc_money(20);
                    }
                }
                else if (Input.GetKeyDown(KeyCode.Mouse0) && PlayerResource.Instance != null)
                {
                    if (PlayerResource.Instance.recyclepis(7))
                    {
                        PlayerResource.Instance.inc_money(20);
                    }
                }
            }

            if (objectHitByRaycast.CompareTag("RAbox"))
            {
                if (Input.GetKeyDown(KeyCode.E) && PlayerResource.Instance != null)
                {
                    if (PlayerResource.Instance.recycleanti(1))
                    {
                        PlayerResource.Instance.inc_money(10);
                    }
                }
                else if (Input.GetKeyDown(KeyCode.Mouse0) && PlayerResource.Instance != null)
                {
                    if (PlayerResource.Instance.recycleanti(1))
                    {
                        PlayerResource.Instance.inc_money(10);
                    }
                }
            }
        }
    }
    public void average_antido(){
        sceneName = SceneManager.GetActiveScene().name;
        datacollation = GetComponent<Datacollection>();
        count_cured = curedCount.Instance.count;

        int average = antido_use / count_cured;

        if(!datasent){
            Debug.Log("Calling SendAverage Antido for  event...");
            datacollation.SendAntido_usage(sceneName + ": " + average);
            datasent = true;
        }


    }
}
