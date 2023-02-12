using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{

    [Header("AudioSources")]
    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private AudioSource _soundSource;
    
    [Header("AudioSoundData")]
    [SerializeField] private SFXLib _sfxLib;
    [SerializeField] private MusicLib _musicLib;

    public SFXLib SFXLib { get => _sfxLib; private set => _sfxLib = value; }
    public MusicLib MusicLib { get => _musicLib; private set => _musicLib = value; }

    public void PlayMusic(AudioClip clip)
    {
        if (_musicSource.isPlaying)
        {
            _musicSource.Stop();
        }

        _musicSource.clip = clip;
        _musicSource.Play();
    }

    public void PlaySound(AudioClip clip, float vol = 1)
    {
        _soundSource.PlayOneShot(clip, vol);
    }

}
