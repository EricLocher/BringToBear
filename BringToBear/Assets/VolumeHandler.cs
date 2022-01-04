using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeHandler : MonoBehaviour
{
    public AudioMixer mixer;
    [SerializeField] Slider volumeSlider;

    public AudioMixer SFXmixer;
    [SerializeField] Slider sfxSlider;

    private void Start()
    {
        if(!PlayerPrefs.HasKey("musicVolume"))
        {
            PlayerPrefs.SetFloat("musicVolume", 0.5f);
            Load();
        }
        else Load();
        if (!PlayerPrefs.HasKey("sfxVolume"))
        {
            PlayerPrefs.SetFloat("sfxVolume", 0.5f);
            LoadSFX();
        }
        else Load();
    }

    public void SetVolume(float sliderValue)
    {
        //mixer.SetFloat("musicVolume", Mathf.Log10(sliderValue) * 20);
        AudioListener.volume = volumeSlider.value;
        Save();
    }

    private void Load()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("musicVolume");
    }
    private void Save()
    {
        PlayerPrefs.SetFloat("musicVolume", volumeSlider.value);
    }





    public void SetSFX(float sliderValue)
    {
        //mixer.SetFloat("musicVolume", Mathf.Log10(sliderValue) * 20);
        AudioListener.volume = sfxSlider.value;
        SaveSFX();
    }

    private void LoadSFX()
    {
        sfxSlider.value = PlayerPrefs.GetFloat("sfxVolume");
    }
    private void SaveSFX()
    {
        PlayerPrefs.SetFloat("sfxVolume", sfxSlider.value);
    }
}
