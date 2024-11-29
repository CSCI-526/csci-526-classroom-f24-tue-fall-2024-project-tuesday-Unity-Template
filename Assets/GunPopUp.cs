using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
        if (SceneManager.GetActiveScene().name == "Level3" && collision.gameObject.name.StartsWith("FlamethrowerPowerup"))
        {
            return;
        }

        if (collision.gameObject.name.StartsWith("FlamethrowerPowerup"))
        {
            Debug.Log("Player picked up Flamethrower Powerup");
            powerupMessage = "Acquired Flame power up! \nYou can switch to fire gun by pressing 3 and break the ice wall in one shot!";

            if (!hasShownPopup)
            {
                hasShownPopup = true;
                ShowPopup();
                continueButton.GetComponent<RectTransform>().sizeDelta = new Vector2(continueButton.GetComponent<RectTransform>().sizeDelta.x, 100);

                PauseGame();
            }
        }
        else if (collision.gameObject.name.StartsWith("RocketPowerup"))
        {
            Debug.Log("Player picked up Rocket Powerup");
            powerupMessage = "Acquired Rocket power up! \nYou can switch to rocket gun by pressing 2, jump a longer distance and beware of the explosion!";

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
