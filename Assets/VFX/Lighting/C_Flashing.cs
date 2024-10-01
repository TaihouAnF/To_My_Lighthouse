using UnityEngine;

public class FlashingLight : MonoBehaviour
{
    public Light pointLight;  // Reference to the point light
    public float speed = 2.0f;  // Speed of flashing
    public float intensityMultiplier = 1.0f;  // Control the maximum intensity
    private float baseIntensity;  // Store the initial intensity

    void Start()
    {
        // Ensure we have a reference to the point light component
        if (pointLight == null)
        {
            pointLight = GetComponent<Light>();
        }

        // Store the initial intensity set in the Inspector
        baseIntensity = pointLight.intensity;
    }

    void Update()
    {
        // Use sine function to create a pulsing effect on top of the base intensity
        pointLight.intensity = baseIntensity * (1 + Mathf.Abs(Mathf.Sin(Time.time * speed)) * intensityMultiplier);
    }
}
