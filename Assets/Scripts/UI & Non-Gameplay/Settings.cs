using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine;

public class Settings : MonoBehaviour
{
    [SerializeField] private AudioMixer myMixer;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider SFXSlider;

    private void Start()
    {
        // N?u ?ã l?u giá tr? âm l??ng, t?i chúng
        if (PlayerPrefs.HasKey("musicVolume") || PlayerPrefs.HasKey("SFXVolume"))
        {
            LoadVolume();
        }
        else
        {
            // N?u không có giá tr? l?u, thi?t l?p âm l??ng m?c ??nh
            musicSlider.value = 0.5f;
            SFXSlider.value = 0.5f;
            SetMusicVolume();
            SetSFXVolume();
        }
    }

    // Thi?t l?p âm l??ng nh?c
    public void SetMusicVolume()
    {
        float volume = musicSlider.value;
        myMixer.SetFloat("music", Mathf.Log10(volume) * 20); // Chuy?n ??i sang decibel
        PlayerPrefs.SetFloat("musicVolume", volume); // L?u giá tr?
    }

    // Thi?t l?p âm l??ng hi?u ?ng âm thanh
    public void SetSFXVolume()
    {
        float volume = SFXSlider.value;
        myMixer.SetFloat("SFX", Mathf.Log10(volume) * 20); // Chuy?n ??i sang decibel
        PlayerPrefs.SetFloat("SFXVolume", volume); // L?u giá tr?
    }

    // T?i các giá tr? âm l??ng ?ã l?u
    private void LoadVolume()
    {
        // ??t giá tr? cho thanh tr??t và áp d?ng âm l??ng
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
        SFXSlider.value = PlayerPrefs.GetFloat("SFXVolume");
        SetMusicVolume();
        SetSFXVolume();
    }
}
