using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    private Vector2 lastDirection;

    // Receive direction from the BulletController
    public void ReceiveDirection(Vector2 direction)
    {
        lastDirection = direction;
        HandleDirectionChange();
    }

    void HandleDirectionChange()
    {
        float angle = Mathf.Atan2(lastDirection.y, lastDirection.x) * Mathf.Rad2Deg - 90f;

        Debug.Log("Player shooting direction: " + lastDirection);
    }
}

