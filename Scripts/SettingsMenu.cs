using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{

    public AudioMixer audioMixer;

    public void SetMasterVolume(float volume)
    {
        audioMixer.SetFloat("Master_Volume", Mathf.Log10(volume)*20);
    }

    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat("Music_Volume", Mathf.Log10(volume) * 20);
    }

    public void SetSFXVolume(float volume)
    {
        audioMixer.SetFloat("Sound_Effects_Volume", Mathf.Log10(volume) * 20);
    }

    public void SetInterfaceEffectsVolume(float volume)
    {
        audioMixer.SetFloat("Interface_Effects_Volume", Mathf.Log10(volume) * 20);
    }

}
