using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class RangeUIControal_tutorial : MonoBehaviour
{
    bool disESC = false;
    public GameObject pauseUI;
    public GameObject loseUI;
    public GameObject winUI;
    public GameObject ReloadUI;
    public GameObject STAUI;
    private string sceneName;

    private Datacollection datacollation;

    private SceneTimer sceneTimer;
    private bool datasent = false;  // Flag to control data sending

    private bool timesent = false;
    public void showLoseUI()
    {
        sceneName = SceneManager.GetActiveScene().name;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        disESC = true;
        ReloadUI.SetActive(false);
        loseUI.SetActive(true);
        Time.timeScale = 0f;
        if (!datasent)
        {
            Debug.Log("Calling SendLevelData for lose event...");
            datacollation.SendLevelData(sceneName);
            datasent = true;  // Mark data as sent
        }
    }

    public void showWinUI()
    {
        
        if (winUI.activeSelf)
        {
            
              // Stop the timer when player wins
        
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            disESC = false;
            winUI.SetActive(false);
            ReloadUI.SetActive(true);
            Time.timeScale = 1f;
            
            
        }
        else
        {
            sceneTimer.StopTimer();
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            disESC = true;
            ReloadUI.SetActive(false);
            winUI.SetActive(true);
            Time.timeScale = 0f;
            if(!timesent){
                Debug.Log("Calling SendPlayTimeData for tutorial..");
                datacollation.SendPlayTime(sceneTimer.result);
                timesent = true;
            }
        }
    }

    public void showPulseUI()
    {
        if (!disESC)
        {
            if (pauseUI.activeSelf)
            {
                pauseUI.SetActive(false);
                ReloadUI.SetActive(true);
                Time.timeScale = 1f;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                ReloadUI.SetActive(false);
                pauseUI.SetActive(true);
                Time.timeScale = 0f;
            }
        }
    }

    public void showSTAUI()
    {
        if (winUI.activeSelf)
        {
            winUI.SetActive(false);
            STAUI.SetActive(true);
        }
        else
        {
            return;
        }
    }

    void Start()
    {
        datacollation = GetComponent<Datacollection>();
        sceneTimer = GetComponent<SceneTimer>();
        Time.timeScale = 1f;
        pauseUI.SetActive(false);
        loseUI.SetActive(false);
        winUI.SetActive(false);
        if(STAUI != null)
        {
            STAUI.SetActive(false);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) && !disESC)
        {
            showPulseUI();
        }
    }
}

