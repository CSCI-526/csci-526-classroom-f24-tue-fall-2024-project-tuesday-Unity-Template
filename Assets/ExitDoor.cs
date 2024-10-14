using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitDoor : MonoBehaviour
{
    public GameObject winningText;
    public Canvas resultTextCanvas; 
    private static bool gameOver = false;
    public static bool isGameOver()
    {
        return gameOver;
    }

    private void Start()
    {
        gameOver=false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player has reached the exit door!");
            if (!gameOver && BulletCollision.isGameOver() == false)
            {
                gameOver = true;                
                Instantiate(winningText, new Vector3(0, 0, 0), Quaternion.identity);
                PauseMenuController pauseMenuController = FindObjectOfType<PauseMenuController>();
                pauseMenuController.ShowGamePauseMenuDelay();

                // set the status of this level to completed
                LevelController levelController = FindObjectOfType<LevelController>();
                if (levelController != null)
                {
                    levelController.CompleteLevel();
                }
                else
                {
                    Debug.LogError("LevelController is not available!");
                }
            }
        }
    }
}