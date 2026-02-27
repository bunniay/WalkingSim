using UnityEngine;
using UnityEngine.UI;

public class BrightnessSlider : MonoBehaviour
{
    Slider slider;
    const string Key = "Brightness01";

    void Awake()
    {
        slider = GetComponent<Slider>();

        slider.value = PlayerPrefs.GetFloat(Key, 0.5f);

        slider.onValueChanged.AddListener(OnSliderChanged);
    }

    void OnSliderChanged(float value)
    {
        PlayerPrefs.SetFloat(Key, value);
        PlayerPrefs.Save();
        Debug.Log("Saved Brightness01 = " + value);
    }
}