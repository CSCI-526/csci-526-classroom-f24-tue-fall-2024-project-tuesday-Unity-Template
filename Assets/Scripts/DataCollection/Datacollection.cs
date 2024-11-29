using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using static System.Net.WebRequestMethods;

public class Datacollection : MonoBehaviour
{
    public int current_Level;
    private string formURL = "https://docs.google.com/forms/u/0/d/e/1FAIpQLSd5kCQO7n4KuZ0D2QrxPrIo3l1btaLIidIXJdqxiFAOCQ-EgQ/formResponse";
    private string entryLevelID = "entry.1729445481";
    public void SendLevelData()
    {
        StartCoroutine(PostToGoogleForm(current_Level));
    }

    private IEnumerator PostToGoogleForm(int level)
    {
        WWWForm form = new WWWForm();
        form.AddField(entryLevelID, level.ToString());

        UnityWebRequest request = UnityWebRequest.Post(formURL, form);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("Error submitting form: " + request.error);
        }
        else
        {
            Debug.Log("Form successfully submitted with level: " + level);
        }
    }
}
