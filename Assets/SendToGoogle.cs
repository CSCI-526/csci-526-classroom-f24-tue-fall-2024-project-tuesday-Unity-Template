using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SendToGoogle : MonoBehaviour
{
    private string completionDataURL = "https://docs.google.com/forms/u/0/d/e/1FAIpQLSeE9fx1ahxn9K7xOgH65phVPEZuSrrIO45OKmUT4Z7AwAWZCg/formResponse";
    private string bulletUsageURL = "https://docs.google.com/forms/u/0/d/e/1FAIpQLSdnl738ebF3ed4m0UARsbS2hYSbCEWqHVkKPHYDJMal0bIhIA/formResponse";
    private string rewardDataURL = "https://docs.google.com/forms/u/0/d/e/1FAIpQLScLnT6MliSA3EXORLUVmMKh0Me6ur2KIpAZUeOv-PdBECKB7w/formResponse";

    private long _sessionID;

    private void Awake()
    {
        _sessionID = DateTime.Now.Ticks;
    }
    public void SendCompletionData()
    {
        LevelController levelController = FindObjectOfType<LevelController>();
        int _shootFriend = levelController.getShootFriend();
        int _noBullet = levelController.getNoBullet();
        int _touchLava = levelController.getTouchLava();
        int _failTries = _shootFriend + _noBullet + _touchLava;
        int _winTries = levelController.getWinTries();
        int _restartTries = levelController.getRestartTries();
        int _numOfTries = levelController.getNumOfTries() + _restartTries;
        bool _ifSuccess = levelController.getIfSuccess();
        int levelID = levelController.getCurrentLevelIndex();
        string _levelID = "level" + levelID.ToString();
        
        if (_numOfTries != 0) {
            StartCoroutine(PostCompletionData(_sessionID.ToString(), _levelID, _shootFriend.ToString(), _noBullet.ToString(), _touchLava.ToString(), _failTries.ToString(), _winTries.ToString(), _restartTries.ToString(), _numOfTries.ToString(),  _ifSuccess.ToString()));
        }
    }

    private IEnumerator PostCompletionData(string sessionID, string _levelID, string _shootFriend, string _noBullet, string _touchLava, string _failTries, string _winTries,string _restartTries, string _numOfTries, string _ifSuccess)
    {
        // https://docs.google.com/forms/u/0/d/e/1FAIpQLSeE9fx1ahxn9K7xOgH65phVPEZuSrrIO45OKmUT4Z7AwAWZCg/formResponse
        WWWForm form = new WWWForm();
        form.AddField("entry.2066483761", sessionID);
        form.AddField("entry.26835437", _levelID);
        form.AddField("entry.942131633", _shootFriend);
        form.AddField("entry.1705336972", _noBullet);
        form.AddField("entry.1513035885", _touchLava);
        form.AddField("entry.1708798683", _failTries);
        form.AddField("entry.285909479", _winTries);
        form.AddField("entry.1022432825", _numOfTries);
        form.AddField("entry.697998222", _ifSuccess);
        form.AddField("entry.191438610", _restartTries);
        
        using (UnityWebRequest www = UnityWebRequest.Post(completionDataURL, form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Form upload complete!");
            }
        }
    }

    public void SendBulletUsageData(int _bulletHas, int _bulletUsed){
        LevelController levelController = FindObjectOfType<LevelController>();
        int levelID = levelController.getCurrentLevelIndex();
        string _levelID = "level" + levelID.ToString();
        float _efficiency = 1 - (float)_bulletUsed / (float)_bulletHas;

        StartCoroutine(PostBulletUsageData(_sessionID.ToString(), _levelID, _bulletHas.ToString(), _bulletUsed.ToString(), _efficiency.ToString()));
    }

    private IEnumerator PostBulletUsageData(string sessionID, string _levelID, string _bulletHas, string _bulletUsed, string usageEfficiency){
        // https://docs.google.com/forms/u/0/d/e/1FAIpQLSdnl738ebF3ed4m0UARsbS2hYSbCEWqHVkKPHYDJMal0bIhIA/formResponse
        WWWForm form = new WWWForm();
        form.AddField("entry.2087028876", sessionID);
        form.AddField("entry.1670463780", _levelID);
        form.AddField("entry.607658478", _bulletHas);
        form.AddField("entry.2043367524", _bulletUsed);
        form.AddField("entry.575685225", usageEfficiency);

        using (UnityWebRequest www = UnityWebRequest.Post(bulletUsageURL, form))
        {
            yield return www.SendWebRequest();
            
            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Form upload complete!");
            }
        }
    }


    public void SendRewardData(int _specialGunCollected,int _specialGunUsed,  int _questionBoxTouched){
        LevelController levelController = FindObjectOfType<LevelController>();
        int levelID = levelController.getCurrentLevelIndex();
        string _levelID = "level" + levelID.ToString();

        StartCoroutine(PostRewardData(_sessionID.ToString(), _levelID, _specialGunCollected.ToString(), _specialGunUsed.ToString(), _questionBoxTouched.ToString()));
    }

    private IEnumerator PostRewardData(string sessionID, string _levelID, string _specialGunCollected, string _specialGunUsed, string _questionBoxTouched){
        // https://docs.google.com/forms/u/0/d/e/1FAIpQLScLnT6MliSA3EXORLUVmMKh0Me6ur2KIpAZUeOv-PdBECKB7w/formResponse
        WWWForm form = new WWWForm();
        form.AddField("entry.1857760284", sessionID);
        form.AddField("entry.3636898", _levelID);
        form.AddField("entry.986329896", _specialGunCollected);
        form.AddField("entry.970361396", _specialGunUsed);
        form.AddField("entry.852429595", _questionBoxTouched);

        using (UnityWebRequest www = UnityWebRequest.Post(rewardDataURL, form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Form upload complete!");
            }
        }
    }
}

