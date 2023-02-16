using UnityEngine;

[CreateAssetMenu(menuName = "AudioData/MusicLib", fileName = "MusicLib")]
public class MusicLib : ScriptableObject
{
    [field: SerializeField] public AudioClip TestMusic;
}
