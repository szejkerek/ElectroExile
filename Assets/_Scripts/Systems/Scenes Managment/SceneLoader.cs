using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoader : Singleton<SceneLoader>
{
    public BasicLoadingScreen bls;

    private void Start()
    {
        StartCoroutine(Test());
    }

    private IEnumerator Test()
    {
        while (true)
        {
            bls.FadeOut();
            yield return new WaitForSeconds(15);
            bls.FadeIn();
            yield return new WaitForSeconds(15);
        }
    }

}
