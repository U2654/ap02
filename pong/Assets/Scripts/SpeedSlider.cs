using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;
using TMPro;
public class SpeedSlider : NetworkBehaviour
{
    private Slider slider;
    public TextMeshProUGUI tmpro; 
     private NetworkVariable<float> sliderValue = new(1.0f);

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // set init value of player prefs to slider 
        slider = GetComponent<Slider>(); 
        PlayerPrefs.SetFloat("BallSpeed", slider.value);       
        PlayerPrefs.Save();

        sliderValue.OnValueChanged += UpdateSlider;
        if (IsServer)
        {
            sliderValue.Value = slider.value;
        }
    }

    public void OnSliderValueChanged(float value)
    {
        PlayerPrefs.SetFloat("BallSpeed", value);
        PlayerPrefs.Save();
        if (IsServer)
        {
            sliderValue.Value = slider.value;
        }
    }

    private void UpdateSlider(float previous, float current)
    {
        slider.value = sliderValue.Value;  
        tmpro.text = "Ball speed " + slider.value.ToString();
    }
}
