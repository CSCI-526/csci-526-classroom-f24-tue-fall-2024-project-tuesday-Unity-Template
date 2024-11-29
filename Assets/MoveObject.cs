using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour
{
    // Start is called before the first frame update

    public float speed = 1.0f;
    public float leftBoundary = -10.0f;
    public float rightBoundary = 10.0f;

    private bool movingRight = true;

    // Update is called once per frame
    void Update()
    {
        if (movingRight)
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
            if (transform.position.x >= rightBoundary)
            {
                movingRight = false;
            }
        }
        else
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
            if (transform.position.x <= leftBoundary)
            {
                movingRight = true;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (gameObject.tag == "MovingPlatform" && collision.gameObject.CompareTag("Player"))
        {
            // Set the player as a child of the platform when they collide
            collision.transform.SetParent(transform);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (gameObject.tag == "MovingPlatform" && collision.gameObject.CompareTag("Player"))
        {
            // Remove the player as a child of the platform when they stop colliding
            collision.transform.SetParent(null);
        }
    }
}
