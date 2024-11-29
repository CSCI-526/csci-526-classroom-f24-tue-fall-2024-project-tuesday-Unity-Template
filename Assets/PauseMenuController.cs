using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class PauseMenuController : MonoBehaviour
{
    public GameObject pauseMenuUI;
    [SerializeField] private Canvas Canvas;
    [SerializeField] private GameObject buttonPanel;
    public GameObject losingText;
    public GameObject winningText;
    public Button pauseButton; 
    public Button continueButton;
    public Button NextLevelButton;
    private bool isPaused = false;
    private bool gameOver = false;
    private LevelController levelController;

    void Start()
    {
        levelController = FindObjectOfType<LevelController>();
        pauseMenuUI.SetActive(false);
        NextLevelButton.gameObject.SetActive(false);

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
    public void ShowGamePauseMenu(bool isWin, string message)
    {

        if (!isPaused)
        {
            TogglePauseMenu();
        }
        ShowGameResult(isWin, message);
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (currentSceneIndex < SceneManager.sceneCountInBuildSettings - 1 && isWin)
        {
            NextLevelButton.gameObject.SetActive(true);
        }
        else
        {
            NextLevelButton.gameObject.SetActive(false);
        }


    }

    public void EndGame()
    {
        gameOver = true;
        continueButton.gameObject.SetActive(false);
    }

    public void ShowGameResult(bool isWin, string message)
    {
        GameObject textPrefab = isWin ? winningText : losingText;
        GameObject instantiatedText = Instantiate(textPrefab, buttonPanel.transform);
        TextMeshProUGUI textComponent = instantiatedText.GetComponent<TextMeshProUGUI>();
        textComponent.text = message;

        Color winTextColor;
        ColorUtility.TryParseHtmlString("#3B892E", out winTextColor); 
        Color loseTextColor;
        ColorUtility.TryParseHtmlString("#E52100", out loseTextColor);
        if (isWin) {
            textComponent.color = winTextColor;
        } else {
            textComponent.color = loseTextColor;
        }
        textComponent.fontSize = 48;  
        textComponent.enableWordWrapping = false; 
        textComponent.overflowMode = TMPro.TextOverflowModes.Overflow; 

        RectTransform rectTransform = instantiatedText.GetComponent<RectTransform>();
        rectTransform.anchorMin = new Vector2(0f, 1f);  
        rectTransform.anchorMax = new Vector2(0f, 1f); 
        rectTransform.pivot = new Vector2(0.5f, 0.5f); 
        rectTransform.anchoredPosition = new Vector2(625f, -30f);
        rectTransform.sizeDelta = new Vector2(800f, 20f);
        rectTransform.localScale = Vector3.one;
        instantiatedText.transform.SetAsFirstSibling();
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
        levelController.SendGooglePlayerLocationData();
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
        Time.timeScale = 1;
        levelController.SendGooglePlayerLocationData();
        SceneManager.LoadScene("MainMenu");
        levelController.SendGoogleCompletionData();
    }

    public void NextLevel()
    {
        Time.timeScale = 1;
        levelController.SendGoogleCompletionData();
        levelController.SendGooglePlayerLocationData();
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        Debug.Log("currentSceneIndex: " + currentSceneIndex);
        int nextSceneIndex = currentSceneIndex + 1;
        Debug.Log("nextSceneIndex: " + nextSceneIndex);
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.Log("No more levels available!");
        }
    }
}