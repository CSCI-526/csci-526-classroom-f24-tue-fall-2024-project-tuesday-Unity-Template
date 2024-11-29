using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ExplosionDamage : MonoBehaviour
{
    // Start is called before the first frame update
    private static bool gameFail = false;

    public GameObject losingText;

    void Start()
    {
        gameFail = false;
    }

    public static bool isGameOver()
    {
        return gameFail;
    }

    public static void setGameOver(bool status)
    {
        gameFail = status;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Explosion collided with " + other.gameObject.tag);
        if (other.gameObject.tag == "NPC")
        {
            gameFail = true;
            PauseMenuController pauseMenuController = FindObjectOfType<PauseMenuController>();
            pauseMenuController.ShowGamePauseMenu(false, "Game Over\nYou exploded a friend :(");
            pauseMenuController.EndGame();
            LevelController levelController = FindObjectOfType<LevelController>();
            levelController.increShootFriend();
            levelController.increNumOfTries();
            other.GetComponent<SpriteRenderer>().color = Color.grey;
        }
        if (other.gameObject.tag == "BreakableWall")
        {
            Destroy(other.gameObject);
        }
    }
}
