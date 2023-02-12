using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class DeveloperOptions
{
    private const string _developerFolder = "Developer/";

    [MenuItem(_developerFolder + "Test")]
    public static void Test()
    {
        AudioManager.Instance.PlaySound(AudioManager.Instance.SFXLib.TestSound);
        Debug.Log("Test");
    }
}

