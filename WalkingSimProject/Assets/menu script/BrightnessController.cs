using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class BrightnessController : MonoBehaviour
{
    public Volume volume;
    private ColorAdjustments colorAdjustments;

    public float minExposure = -3f;
    public float maxExposure = 3f;

    const string Key = "Brightness01";

    void Start()
    {
        if (volume == null)
            volume = FindFirstObjectByType<Volume>();

        if (volume == null || volume.profile == null)
            return;

        if (volume.profile.TryGet(out colorAdjustments))
        {
            float savedValue = PlayerPrefs.GetFloat(Key, 0.5f);
            Apply(savedValue);
        }
    }

    public void Apply(float sliderValue)
    {
        if (colorAdjustments != null)
        {
            float exposure = Mathf.Lerp(minExposure, maxExposure, sliderValue);
            colorAdjustments.postExposure.value = exposure;
        }
    }
}