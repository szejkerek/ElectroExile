using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Helpers
{
    /// <summary>
    /// Helpers/Extensions
    /// 
    /// Sample usage:
    /// transform.DestroyChildren();
    /// </summary>
    public static void DestroyChildern(this Transform t)
    {
        foreach (Transform child in t)
        {
            Object.Destroy(child);
        }
    }
}
