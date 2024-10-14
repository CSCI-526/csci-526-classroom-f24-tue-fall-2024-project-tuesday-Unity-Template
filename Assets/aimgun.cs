using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aimgun : MonoBehaviour
{
    Transform gun;

    // Start is called before the first frame update
    void Start()
    {
        gun = gameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mousePosition - transform.position;
        float angle = Vector2.SignedAngle(Vector2.right, direction);
        gun.transform.eulerAngles = new Vector3(0, 0, angle);
    }
}
