using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DamageEffect : MonoBehaviour
{
    public float flashDuration = 0.5f;  // Total duration of the flash effect
    public Image effectImage;

    void Start()
    {
        if (effectImage == null)
        {
            effectImage = GetComponent<Image>();
        }
        SetTransparent();  // Ensure it starts transparent
    }

    public void TriggerFlash()
    {
        StartCoroutine(DoFlash());
    }

    private IEnumerator DoFlash()
    {
        // Flash in
        float time = 0;
        while (time < flashDuration / 2)
        {
            time += Time.deltaTime;
            float alpha = Mathf.Lerp(0f, 1f, time / (flashDuration / 2));
            SetAlpha(alpha);
            yield return null;
        }

        // Flash out
        time = 0;
        while (time < flashDuration / 2)
        {
            time += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, time / (flashDuration / 2));
            SetAlpha(alpha);
            yield return null;
        }

        SetTransparent();  // Ensure it ends transparent
    }

    private void SetAlpha(float alpha)
    {
        if (effectImage != null)
        {
            var color = effectImage.color;
            color.a = alpha;
            effectImage.color = color;
        }
    }

    private void SetTransparent()
    {
        SetAlpha(0);
    }
}