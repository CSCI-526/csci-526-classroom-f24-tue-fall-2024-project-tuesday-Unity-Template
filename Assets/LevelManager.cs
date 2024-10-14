using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private Button[] levelButtons;
    public Color unlockedColor = Color.green;
    public Color lockedColor = Color.gray;
    public Color completedColor = Color.yellow;

    private const string LevelStatusPrefix = "LevelStatus_";
    private const int Locked = 0;
    private const int Unlocked = 1;
    private const int Completed = 2;

    public static LevelManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Debug.Log("LevelManager already exists, destroying this one.");
            // Destroy(gameObject);
        }
    }
    void Start()
    {
        Debug.Log("LevelManager Start method called.");
        levelButtons = GameObject.FindObjectsOfType<Button>();
        // ResetAllLevelStatus();
        InitializeLevelStatus();
        UpdateButtonStates();
    }

    void InitializeLevelStatus()
    {
        if (!PlayerPrefs.HasKey(LevelStatusPrefix + "0"))
        {
            PlayerPrefs.SetInt(LevelStatusPrefix + "0", Unlocked);
            PlayerPrefs.Save();
        }
    }
    public void ResetAllLevelStatus()
    {
        for (int i = 0; i < levelButtons.Length; i++)
        {
            if (i == 0)
            {
                SetLevelStatus(i, Unlocked);
            }
            else
            {
                SetLevelStatus(i, Locked);
            }
        }
        UpdateButtonStates();
        PlayerPrefs.Save();
    }

    void UpdateButtonStates()
    {
        for (int i = 0; i < levelButtons.Length; i++)
        {
            int status = GetLevelStatus(i);
            Button button = levelButtons[i];
            Text buttonText = button.GetComponentInChildren<Text>();

            switch (status)
            {
                case Locked:
                    button.interactable = false;
                    button.image.color = lockedColor;
                    break;
                case Unlocked:
                    button.interactable = true;
                    button.image.color = unlockedColor;
                    break;
                case Completed:
                    button.interactable = true;
                    button.image.color = completedColor;
                    break;
            }

            int level = i;
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() => LoadLevel(level));
        }
    }

    void LoadLevel(int levelIndex)
    {
        SceneManager.LoadScene("Level" + levelIndex);
    }

    public void CompleteLevel(int levelIndex)
    {
        if (levelIndex >= 0 && levelIndex < levelButtons.Length)
        {
            SetLevelStatus(levelIndex, Completed);
            if (levelIndex + 1 < levelButtons.Length)
            {
                SetLevelStatus(levelIndex + 1, Unlocked);
            }
            UpdateButtonStates();
        }
    }

    int GetLevelStatus(int levelIndex)
    {
        return PlayerPrefs.GetInt(LevelStatusPrefix + levelIndex, Locked);
    }

    void SetLevelStatus(int levelIndex, int status)
    {
        PlayerPrefs.SetInt(LevelStatusPrefix + levelIndex, status);
        PlayerPrefs.Save();
    }
}
