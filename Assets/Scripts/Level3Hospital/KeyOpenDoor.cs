using System.Collections;
using UnityEngine;
using TMPro;

public class KeyOpenDoor : MonoBehaviour
{
    public Transform player; 
    public TMP_Text promptText; 
    public float interactionDistance = 7f; 
    public float displayDuration = 3f; 


    private bool promptShown = false; 
    private bool promptDisplayedOnce = false; 


    void Start()
    {
        if (promptText != null)
        {
            promptText.gameObject.SetActive(false); 
        }
    }

    void Update()
    {
        if (promptDisplayedOnce) return;
        float distanceToPlayer = Vector3.Distance(player.position, transform.position);

        if (distanceToPlayer <= interactionDistance && !promptShown)
        {
            ShowPrompt();
        }
       
    }

    private void ShowPrompt()
    {
        if (promptText != null)
        {
            promptText.gameObject.SetActive(true);
            promptShown = true;
            StartCoroutine(HidePromptAfterDuration());

        }
    }

    private IEnumerator HidePromptAfterDuration()
    {
        yield return new WaitForSeconds(displayDuration);

        if (promptText != null)
        {
            promptText.gameObject.SetActive(false);
        }

        promptDisplayedOnce = true;
    }
}
