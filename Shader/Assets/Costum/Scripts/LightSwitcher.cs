using UnityEngine;

/// <summary>
/// 昼夜交替控制器，控制太阳和月亮的光照切换及天空盒同步
/// </summary>
public class LightSwitcher : MonoBehaviour
{
    [Header("References")]
    public Light sunLight;
    public Light moonLight;
    public Material skyMaterial;

    [Header("Settings")]
    public float rotationSpeed = 5f;
    public float maxSunIntensity = 1.2f;
    public float maxMoonIntensity = 0.3f;

    [Header("Smooth Transition")]
    public float transitionBuffer = 0.15f;

    private string shaderPropertyName = "_SunDirection";

    void Update()
    {
        transform.Rotate(Vector3.right, rotationSpeed * Time.deltaTime);

        if (skyMaterial != null)
        {
            skyMaterial.SetVector(shaderPropertyName, transform.forward);
        }

        float sunHeight = -transform.forward.y;

        sunLight.enabled = sunHeight > -transitionBuffer;
        if (sunLight.enabled)
        {
            sunLight.intensity = Mathf.Clamp01(sunHeight / transitionBuffer) * maxSunIntensity;
        }

        moonLight.enabled = sunHeight < transitionBuffer;
        if (moonLight.enabled)
        {
            moonLight.intensity = Mathf.Clamp01(-sunHeight / transitionBuffer) * maxMoonIntensity;
        }
    }
}