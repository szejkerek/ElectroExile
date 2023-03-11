using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoader : Singleton<SceneLoader>
{
    public LoadingScreen bls;

    private void Start()
    {
        StartCoroutine(Test());
    }

    private IEnumerator Test()
    {
        while (true)
        {
            StartCoroutine(bls.FadeOut());
            yield return new WaitForSeconds(10);
            StartCoroutine(bls.FadeIn());
            yield return new WaitForSeconds(10);
        }
    }

}
