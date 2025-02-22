using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;
using TMPro;
public class WinScoreSlider : NetworkBehaviour
{
    private Slider slider;

    public TextMeshProUGUI tmpro; 

     private NetworkVariable<int> sliderValue = new(7);
 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // set init value of player prefs to slider 
        slider = GetComponent<Slider>(); 
        PlayerPrefs.SetInt("WinScore", (int) slider.value);       
        PlayerPrefs.Save();

        sliderValue.OnValueChanged += UpdateSlider;
        if (IsServer)
        {
            sliderValue.Value = (int) slider.value;
        }
    }

    public void OnSliderValueChanged(float value)
    {
        PlayerPrefs.SetInt("WinScore", (int) value);
        PlayerPrefs.Save();

        if (IsServer)
        {
            sliderValue.Value = (int) slider.value;
        }
    }
    private void UpdateSlider(int previous, int current)
    {
        slider.value = sliderValue.Value;  
        tmpro.text = "Winning score " + slider.value.ToString();
    }

}
