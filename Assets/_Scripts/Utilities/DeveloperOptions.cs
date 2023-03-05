using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class DeveloperOptions
{
    private const string _developerFolder = "Developer/";

    [MenuItem(_developerFolder + "Player/Teleport to spawnpoint")]
    public static void TeleportToSpawnPoint()
    {
        Vector2 spawnpointPosition = SpawnPoint.Instance.transform.position;
        Player.Instance.transform.position = spawnpointPosition;
    }
}

