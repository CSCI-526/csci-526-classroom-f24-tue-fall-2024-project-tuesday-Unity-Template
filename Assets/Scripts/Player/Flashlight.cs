using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    public Light flashlight;          
    private bool isFlashlightOn = false;
    public float maxDistance = 40f;     

    void Start()
    {
        flashlight.enabled = isFlashlightOn;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1)) 
        {
            isFlashlightOn = !isFlashlightOn;
            flashlight.enabled = isFlashlightOn;
        }

        if (isFlashlightOn)
        {
            CheckForZombies();
        }
        else
        {
            ResetZombieIllumination();
        }
    }

    void CheckForZombies()
    {
        RaycastHit hit;
        Ray ray = new Ray(flashlight.transform.position, flashlight.transform.forward);
        Debug.DrawRay(flashlight.transform.position, flashlight.transform.forward * maxDistance, Color.green);

        if (Physics.Raycast(ray, out hit, maxDistance))
        {
            Zombie zombie = hit.collider.GetComponent<Zombie>();
            if (zombie != null)
            {
                zombie.SetIlluminated(true);
            }
        }
        else
        {
            ResetZombieIllumination();
        }
    }

    void ResetZombieIllumination()
    {
        Zombie[] zombies = FindObjectsOfType<Zombie>();
        foreach (Zombie zombie in zombies)
        {
            zombie.SetIlluminated(false);
        }
    }
}
