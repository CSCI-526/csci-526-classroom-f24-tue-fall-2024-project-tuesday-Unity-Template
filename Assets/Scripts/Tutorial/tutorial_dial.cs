using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialDial : MonoBehaviour
{
    public TextMeshProUGUI text;
    public GameObject gate;

    private List<string> begindialog = new List<string> {
        "Use Mouse to move POV; W,A,S,D to move",
        "Left upper corner shows mission; \nRight down corner shows gear info"
    };

    private int index = 0;
    private float timer = 0f;
    public float interval = 5f; // Interval in seconds

    void Start()
    {
        text.text = begindialog[index]; // Initialize with the first dialog
    }

    void Update()
    {
        // Increment the timer based on time passed each frame
        timer += Time.deltaTime;

        // Check if the timer exceeds the interval
        if (timer >= interval)
        {
            timer = 0f; // Reset the timer
            index++;    // Move to the next dialog

            // Check if there are more dialogs left
            if (index < begindialog.Count)
            {
                text.text = begindialog[index];
            }
            else
            {
                Debug.Log("End of tutorial dialog.");
                gate.SetActive(false);
                Destroy(text.gameObject); // Destroy text after dialogs are complete

            }
        }
    }
}
