using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// <c>SoundVolumeChanging</c> is deprecated.
/// DO NOT USE!!!
/// </summary>
public class SoundVolumeChanging : MonoBehaviour
{
    private const float PERCENT_VOLUME_CHANGE = 0.1f;

    /// <summary>
    /// <c>changeMusicVolume</c> is deprecated.
    /// DO NOT USE!!!
    /// </summary>
    public void changeMusicVolume(float newVolume)
    {
        PlayerPrefs.SetFloat("musicVolume", newVolume);
        AudioListener.volume = PlayerPrefs.GetFloat("musicVolume");
    }

    //SoundVolumeChanging sv = new SoundVolumeChanging();

    /// <summary>
    /// <c>MusicVolumeUp</c> is deprecated.
    /// DO NOT USE!!!
    /// </summary>
    public void MusicVolumeUp()
    {
        var vol = PlayerPrefs.GetFloat("musicVolume");
        if (PlayerPrefs.GetFloat("musicVolume") <= 0.9f)
        {
            AudioListener.volume = vol + 0.1f;
        }
    }

    /// <summary>
    /// <c>MusicVolumeDown</c> is deprecated.
    /// DO NOT USE!!!
    /// </summary>
    public void MusicVolumeDown()
    {
        var vol = PlayerPrefs.GetFloat("musicVolume");
        if (PlayerPrefs.GetFloat("musicVolume") >= 0.1f)
        {
            AudioListener.volume = vol - 0.1f;
        }
    }
}
