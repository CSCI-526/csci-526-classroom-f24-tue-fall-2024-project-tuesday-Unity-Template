using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GunPopUp : MonoBehaviour
{
    public GameObject gunPopUps;
    public TextMeshProUGUI popupText;
    public Button continueButton;
    public string powerupMessage;
    private bool hasShownPopup = false;
    private bool isPaused = false;

    private void Start()
    {
        gunPopUps.SetActive(false);
        continueButton.GetComponent<RectTransform>().sizeDelta = new Vector2(continueButton.GetComponent<RectTransform>().sizeDelta.x, 100);
        continueButton.onClick.AddListener(HidePopup);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.StartsWith("FlamethrowerPowerup") && SceneManager.GetActiveScene().name == "Level2")
        {
            Debug.Log("Player picked up Flamethrower Powerup");
            powerupMessage = "Press 2 to use fire gun to break the ice wall in one shot!";

            if (!hasShownPopup)
            {
                hasShownPopup = true;
                ShowPopup();
                continueButton.GetComponent<RectTransform>().sizeDelta = new Vector2(continueButton.GetComponent<RectTransform>().sizeDelta.x, 100);

                PauseGame();
            }
        }
        else if (collision.gameObject.name.StartsWith("RocketPowerup") && SceneManager.GetActiveScene().name == "Level3")
        {
            Debug.Log("Player picked up Rocket Powerup");
            powerupMessage = "Press 3 to use rocket gun to jump further and beware of explosions!";

            if (!hasShownPopup)
            {
                hasShownPopup = true;
                ShowPopup();
                PauseGame();
            }
        }
    }

    private void ShowPopup()
    {
        popupText.text = powerupMessage;
        gunPopUps.SetActive(true);
    }

    private void HidePopup()
    {
        gunPopUps.SetActive(false);
        // hasShownPopup = false;
        ResumeGame();
    }
    private void PauseGame()
    {
        Time.timeScale = 0f;
        isPaused = true;
    }
    private void ResumeGame()
    {
        Time.timeScale = 1f;
        isPaused = false;
    }

    public bool IsPaused()
    {
        return isPaused;
    }

}
