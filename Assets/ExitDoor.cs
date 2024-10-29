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
                pauseMenuController.EndGame();
                LevelController levelController = FindObjectOfType<LevelController>();
                if (levelController != null)
                {
                    levelController.increWinTries();
                    levelController.setIfSuccess();
                    levelController.increNumOfTries();
                    levelController.SendGoogleBulletUsageData();
                    levelController.SendGoogleRewardData();
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