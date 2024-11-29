using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BulletController : MonoBehaviour
{
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
    // Reference to the bullet prefab
    public GameObject bulletPrefab;

    // Bullet speed
    public float bulletSpeed = 10f;

    // Bullet count
    private int bulletCount = 0;

    // Reference to the player's controller
    private PlayerController playerController;

    void Update()
    {
        // Check for left mouse button click
        // do this only if the game is not paused
        
        if (Input.GetMouseButtonDown(0))
        {
            ShootBullet();
        }
    }

    // Function to spawn and shoot bullet
    void ShootBullet()
    {
        // Get the mouse position in world space
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0; // Set z to 0 since we're in 2D

        // Get the player's position (where the bullet will spawn)
        Vector3 playerPosition = transform.position;

        // Instantiate the bullet at the player's position
        GameObject bullet = Instantiate(bulletPrefab, playerPosition, Quaternion.identity);

        // Calculate the directionion
        Vector2 direction = (mousePosition - playerPosition).normalized;

        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = direction * bulletSpeed;
        }

        // Pass the direction to the player's controller
        if (playerController != null)
        {
            playerController.ReceiveDirection(direction);
        }

        // Increment the bullet count; maybe we should do this by calling a function in the game manager
        bulletCount++;
        Debug.Log("Bullet count: " + bulletCount);

    }
}
