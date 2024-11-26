using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class securityCamera : MonoBehaviour
{
    public Vector3 startRotation = new Vector3(28.925f, -227.8f, -10.69f);  
    public Vector3 endRotation = new Vector3(26.679f, -174.3f, 15.74f);     
    public float rotationSpeed = 1.0f;                                      

    private Quaternion startQuaternion;
    private Quaternion endQuaternion;
    private bool rotatingToEnd = true;  

    void Start()
    {
        startQuaternion = Quaternion.Euler(startRotation);
        endQuaternion = Quaternion.Euler(endRotation);

        transform.rotation = startQuaternion;
    }

    void Update()
    {
        if (rotatingToEnd)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, endQuaternion, rotationSpeed * Time.deltaTime);

            if (Quaternion.Angle(transform.rotation, endQuaternion) < 0.1f)
            {
                rotatingToEnd = false;
            }
        }
        else
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, startQuaternion, rotationSpeed * Time.deltaTime);

            if (Quaternion.Angle(transform.rotation, startQuaternion) < 0.1f)
            {
                rotatingToEnd = true;
            }
        }
    }
}
