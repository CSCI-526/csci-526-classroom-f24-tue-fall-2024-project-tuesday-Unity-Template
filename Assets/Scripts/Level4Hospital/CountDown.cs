using UnityEngine;

public class CountDown : MonoBehaviour
{
    public TextMesh countdownTextMesh; 
    private float countdownTime = 30f;

    void Start()
    {
        UpdateCountdownText();
    }

    void Update()
    {
        if (countdownTime > 0)
        {
            countdownTime -= Time.deltaTime;
            UpdateCountdownText();
        }
        else
        {
            countdownTextMesh.text = "0"; 
        }
    }

    void UpdateCountdownText()
    {
        countdownTextMesh.text = Mathf.Ceil(countdownTime).ToString(); 
    }
}
