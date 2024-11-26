/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;  

public class TechingKey : MonoBehaviour
{
    public TMP_Text promptText; 
    public float displayDuration = 4f;  

    private void Start()
    {
        if (promptText != null)
        {
            promptText.gameObject.SetActive(true);
            StartCoroutine(HidePromptAfterDelay());
        }
    }

    private IEnumerator HidePromptAfterDelay()
    {
        yield return new WaitForSeconds(displayDuration);

        if (promptText != null)
        {
            promptText.gameObject.SetActive(false);
        }
    }
}*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TechingKey : MonoBehaviour
{
    public TMP_Text promptText1; // 第一个提示文本
    public TMP_Text promptText2; // 第二个提示文本
    public float displayDuration = 4f;    // 每个提示的显示时间
    public float delayBetweenPrompts = 2f; // 提示之间的延迟

    private void Start()
    {
        if (promptText1 != null && promptText2 != null)
        {
            promptText1.gameObject.SetActive(false);
            promptText2.gameObject.SetActive(false);
            StartCoroutine(ShowPromptsInSequence());
        }
    }

    private IEnumerator ShowPromptsInSequence()
    {
        // 显示第一个提示
        promptText1.gameObject.SetActive(true);
        yield return new WaitForSeconds(displayDuration);
        promptText1.gameObject.SetActive(false);

        // 等待一段时间后显示第二个提示
        yield return new WaitForSeconds(delayBetweenPrompts);
        promptText2.gameObject.SetActive(true);
        yield return new WaitForSeconds(displayDuration);
        promptText2.gameObject.SetActive(false);
    }
}
