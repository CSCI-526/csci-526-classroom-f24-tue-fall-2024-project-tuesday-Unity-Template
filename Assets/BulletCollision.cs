using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class BulletCollision : MonoBehaviour
{
    public GameObject losingText;
    private static bool gameOver = false;
    // public static bool gameOver = false;
    public GameObject explosionPrefab;
    private void Start()
    {
        gameOver = false;
    }

    public static bool isGameOver()
    {
        return gameOver;
    }

    public static void setGameOver(bool status)
    {
        gameOver = status;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the bullet collided with an NPC
        if (other.CompareTag("NPC"))
        {
            Debug.Log("Bullet collided with NPC");
            if (!gameOver)
            {
                gameOver = true;
                Instantiate(losingText, new Vector3(0, 0, 0), Quaternion.identity);
                PauseMenuController pauseMenuController = FindObjectOfType<PauseMenuController>();
                pauseMenuController.ShowGamePauseMenuDelay();
            }
            // change the color of the NPC to grey
            other.GetComponent<SpriteRenderer>().color = Color.grey;
        }
        else if (other.CompareTag("BreakableWall"))
        {
            Debug.Log("Bullet collided with Breakable Wall");
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("icewall"))
        {
            Debug.Log("Bullet collided with ice wall");
            icewall wall = other.GetComponent<icewall>();
            if (wall != null)
            {
                if(gameObject.name == "FireBullet(Clone)")
                {
                    wall.RegisterHit(wall.requiredHits);
                } else
                {
                    wall.RegisterHit();
                }
            }
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("Bullet collided with " + other.name);
        }

        // Destroy the bullet after it hits anything
        if (other.name != "PlayerObject" && other.tag != "GunPowerup")
        {
            if (gameObject.name == "RocketBullet(Clone)")
            {
                GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
                Destroy(explosion, 1.5f);
            }
            Destroy(gameObject);
        }
    }
}
