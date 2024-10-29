using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FireGun : MonoBehaviour
{
    public GameObject gunPopUps;           
    public TextMeshProUGUI popupText;        
    public Button continueButton;           
    public string powerupMessage;
    private bool hasShownPopup = false;     

    private void Start()
    {
        gunPopUps.SetActive(false);        
        continueButton.onClick.AddListener(HidePopup);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (SceneManager.GetActiveScene().name == "Level3" && collision.gameObject.name == "FlamethrowerPowerup")
        {
            return; 
        }

        if (collision.gameObject.name == "FlamethrowerPowerup")
        {
            Debug.Log("Player picked up Flamethrower Powerup");
            powerupMessage = "Acquire the flame power up! Now you can shift to fire gun by clicking 3 and break the ice wall with one shot!";

            if (!hasShownPopup)
            {
                hasShownPopup = true;
                ShowPopup();
                PauseGame();
            }
        }
        else if (collision.gameObject.name == "RocketPowerup")
        {
            Debug.Log("Player picked up Rocket Powerup");
            powerupMessage = "Acquire the Rocket power up! Now you can shift to rocket gun by clicking 2 and create wider attacks!";

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
        hasShownPopup = false;
        ResumeGame();           
    }
    private void PauseGame()
    {
        Time.timeScale = 0f; 
    }
    private void ResumeGame()
    {
        Time.timeScale = 1f; 
    }

}
