using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class PauseMenuController : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public Button pauseButton; 
    public Button continueButton;
    private bool isPaused = false;
    private bool gameOver = false;
    private LevelController levelController;

    void Start()
    {
        levelController = FindObjectOfType<LevelController>();
        pauseMenuUI.SetActive(false);

        if (pauseButton != null)
        {
            pauseButton.onClick.AddListener(TogglePauseMenu);
        }
        else
        {
            Debug.LogError("Pause button is not assigned!");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePauseMenu();
        }
    }

    // get isPaused status
    public bool IsPaused()
    {
        return isPaused;
    }

    public void TogglePauseMenu()
    {
        isPaused = !isPaused;
        pauseMenuUI.SetActive(isPaused);
        Time.timeScale = isPaused ? 0 : 1;

        if (pauseButton != null)
        {
            pauseButton.gameObject.SetActive(!isPaused);
        }
    }
    public void ShowGamePauseMenu()
    {
        if (!isPaused)
        {
            TogglePauseMenu();
        }
    }

    public void ShowGamePauseMenuDelay()
    {
        StartCoroutine(ShowGamePauseMenuCoroutine());
    }
    private IEnumerator ShowGamePauseMenuCoroutine(){
        yield return new WaitForSeconds(1f);

        if (!isPaused)
        {
            TogglePauseMenu();
        }
    }

    public void EndGame()
    {
        gameOver = true;
        continueButton.gameObject.SetActive(false);
    }

    public void Continue()
    {
        if (!gameOver) {
           TogglePauseMenu(); 
        } else {
            return;
        }
        
    }

    public void Restart()
    {
        levelController.increRestartTries();
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
        levelController.SendGoogleCompletionData();
    }
}