using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// <c>SceneChanger</c> is deprecated.
/// DO NOT USE!!!
/// </summary>
public class SceneChanger : MonoBehaviour
{
    /// <summary>
    /// <c>ChangeScene</c> is deprecated.
    /// DO NOT USE!!!
    /// </summary>
    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    /// <summary>
    /// <c>Exit</c> is deprecated.
    /// DO NOT USE!!!
    /// </summary>
    public void Exit()
    {
        Application.Quit();
    }
}
