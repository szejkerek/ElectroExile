using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class DeveloperOptions : MonoBehaviour
{
//Keep all dev options here to be able to create builds
#if UNITY_EDITOR

    private const string _developerFolder = "Developer/";

    [MenuItem(_developerFolder + "Player/Teleport to spawnpoint")]
    public static void TeleportToSpawnPoint()
    {
        Player.Instance.MoveToSpawnPoint();
    }

#endif
}

