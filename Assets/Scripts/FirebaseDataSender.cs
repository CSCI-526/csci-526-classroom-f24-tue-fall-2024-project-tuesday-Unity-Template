using UnityEngine;
using System.Collections;
using System.Text;
using UnityEngine.Networking;
using System.Collections.Generic;

public class FirebaseDataSender : MonoBehaviour
{
    private string databaseURL = "https://test-32dcb-default-rtdb.firebaseio.com/";

    public static FirebaseDataSender Instance { get; private set; }
    private int flashlightUsageCount = 0;
    

    private void Awake()
    {
        if (Instance == null)     
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SendGameResult(bool isWin, int finalWave, float totalGameTime, List<float> flashlightDurations)
    {   
        // create data object
        GameResultData data = new GameResultData
        {
            totalGameTime = totalGameTime,
            result = isWin ? "Win" : "Lose",
            finalWave = finalWave,
            timestamp = GetTimestamp(),
            flashlightUsageCount = flashlightDurations.Count,
            flashlightDurations = flashlightDurations
        };

        //  Serialize the data object as JSON
        string json = JsonUtility.ToJson(data);
        Debug.Log("调用 SendGameResult 方法" +json);
        // Starting a concatenation to send data
        StartCoroutine(PostDataToFirebase(json));
        
        
    }

    private IEnumerator PostDataToFirebase(string json)
{
    // 构建请求 URL
    string url = databaseURL + "gameResults.json";

    // 创建 UnityWebRequest，使用 POST
    UnityWebRequest request = new UnityWebRequest(url, "POST");
    byte[] bodyRaw = Encoding.UTF8.GetBytes(json);
    request.uploadHandler = new UploadHandlerRaw(bodyRaw);
    request.downloadHandler = new DownloadHandlerBuffer();

    // 设置请求头
    request.SetRequestHeader("Content-Type", "application/json");

    // 发送请求并等待响应
    yield return request.SendWebRequest();

    if (request.result == UnityWebRequest.Result.Success)
    {
        Debug.Log("数据已成功发送到 Firebase。");
        Debug.Log("响应内容：" + request.downloadHandler.text);
    }
    else
    {
        Debug.LogError("发送数据到 Firebase 时出错: " + request.error);
        Debug.LogError("响应内容：" + request.downloadHandler.text);
    }
}

    private string GetTimestamp()
    {
        return System.DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ");
    }
}

[System.Serializable]
public class GameResultData
{
    public float totalGameTime;
    public string result;
    public int finalWave;
    public string timestamp;
    public int flashlightUsageCount;
    public List<float> flashlightDurations;
}
