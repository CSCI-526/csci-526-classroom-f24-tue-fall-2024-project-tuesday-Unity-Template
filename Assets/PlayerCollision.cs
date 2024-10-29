using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public GameObject losingText;
    private static bool gameOver = false;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag == "lava")
        {
            Debug.Log("Game Over");
            if (!gameOver)
            {
                gameOver = true;
                Instantiate(losingText, new Vector3(0, 0, 0), Quaternion.identity);
                PauseMenuController pauseMenuController = FindObjectOfType<PauseMenuController>();
                pauseMenuController.ShowGamePauseMenuDelay();
                pauseMenuController.EndGame();
            }
        }
    }
}
