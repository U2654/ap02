using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;
using TMPro;
public class SpeedSlider : MonoBehaviour
{
    private Slider slider;
    public TextMeshProUGUI tmpro; 

    private SettingsManager settingsManager;

    private bool isUpdatingSlider = false;

    void Start()
    {
        // set init value of player prefs to slider 
        slider = GetComponent<Slider>(); 
        settingsManager = GameObject.Find("MenuCanvas").GetComponent<SettingsManager>();

        settingsManager.OnSpeedValueChanged += UpdateSlider;
    }

    void OnDestroy()
    {
        // Unsubscribe from the event to avoid memory leaks
        settingsManager.OnSpeedValueChanged -= UpdateSlider;
    }

    public void OnSliderValueChanged(float newValue)
    {
        if (!isUpdatingSlider) 
        {
            isUpdatingSlider = true;
            settingsManager.SetSpeedValueRpc(newValue);
        }
    }

    private void UpdateSlider(float newValue)
    {
        isUpdatingSlider = true;
        slider.value = newValue; 
        tmpro.text = "Ball speed " + slider.value.ToString();
        isUpdatingSlider = false;
    }
}
