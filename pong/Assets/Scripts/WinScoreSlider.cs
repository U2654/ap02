using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;
using TMPro;

public class WinScoreSlider : MonoBehaviour
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

        settingsManager.OnWinScoreValueChanged += UpdateSlider;
    }

    void OnDestroy()
    {
        // Unsubscribe from the event to avoid memory leaks
        settingsManager.OnWinScoreValueChanged -= UpdateSlider;
    }

    public void OnSliderValueChanged(float newValue)
    {
        if (!isUpdatingSlider) 
        {
            isUpdatingSlider = true;
            settingsManager.SetWinScoreValueRpc((int) newValue);
        }
    }

    private void UpdateSlider(int newValue)
    {
        isUpdatingSlider = true;
        slider.value = newValue; 
        tmpro.text = "WinScore " + slider.value.ToString();
        isUpdatingSlider = false;
    }
}
