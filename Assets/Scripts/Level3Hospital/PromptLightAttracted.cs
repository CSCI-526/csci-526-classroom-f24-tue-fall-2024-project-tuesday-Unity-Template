using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;  

public class PromptLightAttracted : MonoBehaviour
{
    public TMP_Text promptText; 
    public float displayDuration = 4f;  // 显示时间
    public float delayBeforeShow = 5f;  // 延迟显示的时间

    private void Start()
    {
        if (promptText != null)
        {
            promptText.gameObject.SetActive(false); // 初始状态为不显示
            StartCoroutine(ShowPromptAfterDelay());
        }
    }

    private IEnumerator ShowPromptAfterDelay()
    {
        yield return new WaitForSeconds(delayBeforeShow);
        Debug.Log("PromptLightAttracted: Showing prompt.");

        if (promptText != null)
        {
            promptText.gameObject.SetActive(true);
            StartCoroutine(HidePromptAfterDelay());
        }
    }

    private IEnumerator HidePromptAfterDelay()
    {
        yield return new WaitForSeconds(displayDuration);     
           Debug.Log("PromptLightAttracted: Hiding prompt.");

        if (promptText != null)
        {
            promptText.gameObject.SetActive(false);
        }
    }
}
