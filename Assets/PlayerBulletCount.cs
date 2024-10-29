using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletCount : MonoBehaviour
{
    public GameObject playerBody;
    private Vector3 offsetAbove = new Vector3(0, 0.8f, 0); 
    private Vector3 offsetBelow = new Vector3(0, -0.8f, 0);
    
    private Camera mainCamera;
    private float screenHeightThreshold = 0.9f;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 screenPosition = mainCamera.WorldToScreenPoint(playerBody.transform.position);
        // place the bullet count right above the playerBody
        if (screenPosition.y > mainCamera.pixelHeight * screenHeightThreshold)
            transform.position = playerBody.transform.position + offsetBelow;
        else
            transform.position = playerBody.transform.position + offsetAbove;

    }
}
