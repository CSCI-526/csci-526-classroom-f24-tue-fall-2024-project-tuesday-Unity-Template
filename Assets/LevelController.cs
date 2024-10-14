using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    private LevelManager levelManager;
    private int currentLevelIndex;
    

    void Start()
    {
        currentLevelIndex = SceneManager.GetActiveScene().buildIndex - 1;
        
        levelManager = FindObjectOfType<LevelManager>();
        if (levelManager == null)
        {
            Debug.LogError("LevelManager not found in the scene!");
        }
    }


    public void CompleteLevel()
    {
        if (levelManager != null)
        {
            levelManager.CompleteLevel(currentLevelIndex);
        }
        else
        {
            Debug.LogError("LevelManager is not available!");
        }
    }
}