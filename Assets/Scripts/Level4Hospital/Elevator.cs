using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    public float moveDistance = 8f;    
    public float speed = 2f;           
    public float waitTime = 4f;        

    private Vector3 startPosition;
    private Vector3 endPosition;
    private bool movingUp = true;     
    private float waitTimer = 0f;

    void Start()
    {
        startPosition = transform.position;

        endPosition = new Vector3(startPosition.x, startPosition.y + moveDistance, startPosition.z);

        transform.position = startPosition;
    }

    void Update()
    {
        if (waitTimer > 0)
        {
            waitTimer -= Time.deltaTime;
            return; 
        }

        if (movingUp)
        {
            transform.position = Vector3.MoveTowards(transform.position, endPosition, speed * Time.deltaTime);
            if (transform.position == endPosition)
            {
                movingUp = false;
                waitTimer = waitTime;
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, startPosition, speed * Time.deltaTime);
            if (transform.position == startPosition)
            {
                movingUp = true;
                waitTimer = waitTime; 
            }
        }
    }
}
