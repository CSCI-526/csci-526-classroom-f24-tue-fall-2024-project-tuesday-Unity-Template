using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    private LevelManager levelManager;
    private int currentLevelIndex;

    private static int _shootFriend;
    private static int _noBullet;

    private static int _touchLava;
    private static int _winTries;
    private static int _restartTries;
    private static int _numOfTries;
    private static bool _ifSuccess;
    

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

    private void ResetLevelData()
    {
        _shootFriend = 0;
        _noBullet = 0;
        _touchLava = 0;
        _winTries = 0;
        _restartTries = 0;
        _numOfTries = 0;
        _ifSuccess = false;
    }

    public void SendGoogleCompletionData(){
        SendToGoogle sendToGoogle = FindObjectOfType<SendToGoogle>();
        sendToGoogle.SendCompletionData();
        ResetLevelData();
    }

    public void SendGoogleBulletUsageData(){
        movePlayer player = FindObjectOfType<movePlayer>();
        int bulletHas = player.bulletLimit;
        int bulletUsed = player.getBulletUsed();

        SendToGoogle sendToGoogle = FindObjectOfType<SendToGoogle>();
        sendToGoogle.SendBulletUsageData(bulletHas, bulletUsed);
    }

    public void SendGoogleRewardData(){
        movePlayer player = FindObjectOfType<movePlayer>();
        int specialGunCollected = player.specialGunCollected;
        int specialGunUsed = player.specialGunUsed;
        int questionBoxTouched = player.questionBoxTouched;

        SendToGoogle sendToGoogle = FindObjectOfType<SendToGoogle>();
        sendToGoogle.SendRewardData(specialGunCollected, specialGunUsed, questionBoxTouched);
    }


    public void increShootFriend(){
        _shootFriend += 1;
    }
    public void increNoBullet(){
        _noBullet += 1;
    }

    public void increTouchLava(){
        _touchLava += 1;
    }

    public void increWinTries(){
        _winTries += 1;
    }
    public void increRestartTries(){
        _restartTries += 1;
    }
    public void increNumOfTries(){
        _numOfTries += 1;
    }
    public void setIfSuccess(){  
        _ifSuccess = true;
    }

    public int getShootFriend(){
        return _shootFriend;
    }
    public int getNoBullet(){
        return _noBullet;
    }
    public int getWinTries(){
        return _winTries;
    }

    public int getRestartTries(){
        return _restartTries;
    }
    public int getNumOfTries(){
        return _numOfTries;
    }
    public bool getIfSuccess(){
        return _ifSuccess;
    }
    public int getCurrentLevelIndex(){
        return currentLevelIndex;
    }

    public int getTouchLava(){
        return _touchLava;
    }
}