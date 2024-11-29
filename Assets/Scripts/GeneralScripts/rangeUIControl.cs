using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rangeUIControl : MonoBehaviour
{
    bool disESC = false;
    public GameObject pauseUI;
    public GameObject loseUI;
    public GameObject winUI;
    public GameObject ReloadUI;
    public GameObject STAUI;
    public void showLoseUI()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        disESC = true;
        ReloadUI.SetActive(false);
        loseUI.SetActive(true);
        Time.timeScale = 0f;
    }

    public void showWinUI()
    {
        
        if (winUI.activeSelf)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            disESC = false;
            winUI.SetActive(false);
            ReloadUI.SetActive(true);
            Time.timeScale = 1f;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            disESC = true;
            ReloadUI.SetActive(false);
            winUI.SetActive(true);
            Time.timeScale = 0f;
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
        Debug.Log("here1");
        if (winUI.activeSelf)
        {
            Debug.Log("here2");
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
        if (Input.GetKeyDown(KeyCode.Escape) && !disESC)
        {
            showPulseUI();
        }
    }
}
