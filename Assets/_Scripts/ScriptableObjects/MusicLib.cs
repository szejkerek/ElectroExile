using UnityEngine;

[CreateAssetMenu(menuName = "AudioData/MusicLib", fileName = "MusicLib")]
public class MusicLib : ScriptableObject
{
    [SerializeField] private AudioClip _testMusic;
    public AudioClip TestMusic => _testMusic;
}
