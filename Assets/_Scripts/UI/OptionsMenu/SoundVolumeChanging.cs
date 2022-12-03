using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundVolumeChanging : MonoBehaviour
{
    private const float PERCENT_VOLUME_CHANGE = 0.1f;
    public void changeMusicVolume(float newVolume)
    {
        PlayerPrefs.SetFloat("musicVolume", newVolume);
        AudioListener.volume = PlayerPrefs.GetFloat("musicVolume");
    }

    public void MusicVolumeUp()
    {
        var vol = PlayerPrefs.GetFloat("musicVolume");
        if (PlayerPrefs.GetFloat("musicVolume") <= 0.9f)
        {
            AudioListener.volume = vol + 0.1f;
        }
    }
    public void MusicVolumeDown()
    {
        var vol = PlayerPrefs.GetFloat("musicVolume");
        if (PlayerPrefs.GetFloat("musicVolume") >= 0.1f)
        {
            AudioListener.volume = vol - 0.1f;
        }
    }
}
