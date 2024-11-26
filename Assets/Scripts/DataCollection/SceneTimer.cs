using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTimer : MonoBehaviour
{
    private float startTime;
    private float endTime;
    public float playTime;

    public string result;
    // Start is called before the first frame update
    void Start()
    {
        StartTimer();
    }
    void StartTimer()
    {
        startTime = Time.time;
        Debug.Log("Timer started at: " + startTime + " seconds");
    }
    public void StopTimer()
    {
        // Record the end time and calculate the play time
        endTime = Time.time;
        playTime = endTime - startTime;
        if((playTime <=60)){
            result = "less then 1 min";
        }
        else if((playTime > 60) && (playTime <= 120)){
            result = "between 1 min and 2 mins";
        }
        else if(playTime > 120){
            result = "more than 2 mins";
        }
        Debug.Log("Timer stopped at: " + endTime + " seconds");
        Debug.Log("Total play time in scene: " + playTime + " seconds");

        // You can save or send this playTime to your Datacollection system if needed
        // datacollection.SendPlayTime(playTime);  // Example function call
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
