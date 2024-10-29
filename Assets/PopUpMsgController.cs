using UnityEngine;
using TMPro;
using System.Collections;

public class PopUpTextManager : MonoBehaviour
{
    private TextMeshProUGUI text;
    public float delay = 1f;

    void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
        text.gameObject.SetActive(false);
    }

    public void ShowText(string message)
    {
        text.text = message;
        text.gameObject.SetActive(true);
        StartCoroutine(HideTextAfterDelay(2f));
    }

    IEnumerator HideTextAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        text.gameObject.SetActive(false);
    }
}
