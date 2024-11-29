using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DayNightCycle : MonoBehaviour
{
    public static DayNightCycle Instance { get; set; }
    private Light directionalLight;  // The directional light to simulate the sun
    public float dayDuration = 120f; // Duration of a full day in seconds
    public Gradient lightColor;     // Gradient for day/night color transitions
    public AnimationCurve lightIntensityCurve; // Curve to control light intensity over time

    private float timeElapsed;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }
    private void Start()
    {
        directionalLight = gameObject.GetComponent<Light>();
    }
    void Update()
    {
        // Calculate the time progression (0 to 1 loop)
        timeElapsed += Time.deltaTime;
        float timeOfDay = (timeElapsed / dayDuration) % 1f;

        // Control light rotation to simulate the sun rising and setting
        directionalLight.transform.localRotation = Quaternion.Euler(new Vector3((timeOfDay * 360f) + 120f, 0, 0));

        // Adjust light color and intensity
        directionalLight.color = lightColor.Evaluate(timeOfDay);
        directionalLight.intensity = lightIntensityCurve.Evaluate(timeOfDay);
    }

    //check if in the night
    public bool IsNight()
    {
        float sunAngle = directionalLight.transform.rotation.eulerAngles.x;
        return sunAngle > 180f && sunAngle < 360f; // 
    }
}
