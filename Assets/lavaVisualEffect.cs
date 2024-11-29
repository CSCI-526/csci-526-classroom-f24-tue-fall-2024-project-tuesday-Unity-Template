using UnityEngine;

public class LavaAlphaController : MonoBehaviour
{
    private static GameObject[] lavaObjects;

    void Start()
    {
        
            }

    public static void UpdateLavaAlpha()
    {
        lavaObjects = GameObject.FindGameObjectsWithTag("lava");
        float brightness = Mathf.Sin(Time.time * 2) * 0.4f + 0.4f + 1f; 
        foreach (GameObject lavaObject in lavaObjects)
        {
            Renderer renderer = lavaObject.GetComponent<Renderer>();
            if (renderer != null)
            {
                Color baseColor = renderer.material.color;

                Color.RGBToHSV(baseColor, out float h, out float s, out float v);
                v = brightness;
                Color adjustedColor = Color.HSVToRGB(h, s, v);

                renderer.material.color = adjustedColor;
            }
        }
    }

   
}
