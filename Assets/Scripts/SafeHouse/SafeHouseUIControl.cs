using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeHouseUIControl : MonoBehaviour
{
    public static SafeHouseUIControl Instance { get; set; }

    bool disESC = false;
    public bool disE = false;
    public GameObject pauseUI;
    public GameObject LevelUI;


    public void showLevelSelection()
    {

        if (LevelUI.activeSelf)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            disESC = false;
            disE = false;
            LevelUI.SetActive(false);
            Time.timeScale = 1f;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            disESC = true;
            disE=true;
            LevelUI.SetActive(true);
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
                Time.timeScale = 1f;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                disE=false;
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                pauseUI.SetActive(true);
                Time.timeScale = 0f;
                disE = true;
            }
        }
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    void Start()
    {
        Time.timeScale = 1f;
        pauseUI.SetActive(false);
        LevelUI.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !disESC)
        {
            showPulseUI();
        }
    }
}
