using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class volumeSlider : MonoBehaviour
{
    [SerializeField] private Slider vSlider;
    [SerializeField] private Text volumeLevel;

    private void Start()
    {
        loadValues();
    }
    public void volSlider(float volume)
    {

        volumeLevel.text = ((int)(volume * 100)).ToString();
    }
    public void saveVolume()
    {
        float volumeVal = vSlider.value;
        PlayerPrefs.SetFloat("VolumeValue", volumeVal);
        loadValues();
    }
    void loadValues()
    {
        float volumeVal = PlayerPrefs.GetFloat("VolumeValue");
        volumeLevel.text = ((int)(volumeVal * 100)).ToString();
        vSlider.value = volumeVal;
        AudioListener.volume = volumeVal; 
    }
}
