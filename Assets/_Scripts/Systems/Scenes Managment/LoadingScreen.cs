using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class LoadingScreen : MonoBehaviour
{
    [Header("Loading Screen")]
    [SerializeField] private float fadeInDuration;
    [SerializeField] private float fadeOutDuration;

    [Header("Progress Bar")]
    [SerializeField] private float progressBarDelay;
    [SerializeField] private float fadeInProgressBarDuration;
    [SerializeField] private float fadeOutProgressBarDuration;


    private Slider progressBar;
    private CanvasGroup loadingScreenCG;
    private CanvasGroup progressBarCG;

    private Tween loadingScreenTween;
    private Tween progressBarTween;

    public float ProgressValue
    {
      get { return progressBar.value; }
      set { progressBar.value = value; } 
    }

    private void Awake()
    {
        progressBar = GetComponentInChildren<Slider>();
        loadingScreenCG = GetComponent<CanvasGroup>();
        progressBarCG = progressBar.GetComponent<CanvasGroup>();
     }

    public IEnumerator FadeIn(bool showProgressBar = true)
    {
        Fade(loadingScreenTween, loadingScreenCG, 1, fadeInDuration);
        if (showProgressBar)
        {
            StartCoroutine(FadeInProgressBarCorutine());
        }
        yield return null;
    }

    public IEnumerator FadeOut()
    {
        StartCoroutine(FadeOutProgressBarCorutine());
        Fade(loadingScreenTween, loadingScreenCG, 0, fadeOutDuration);
        yield return null;
    }

    private IEnumerator FadeInProgressBarCorutine()
    {
        yield return new WaitForSeconds(fadeInDuration + progressBarDelay);
        Fade(progressBarTween, progressBarCG, 1, fadeInProgressBarDuration);
        yield return new WaitForSeconds(fadeInProgressBarDuration);
    }

    private IEnumerator FadeOutProgressBarCorutine()
    {
        Fade(progressBarTween, progressBarCG, 0, fadeOutProgressBarDuration);
        yield return new WaitForSeconds(fadeOutProgressBarDuration);
    }

    private void Fade(Tween tween, CanvasGroup canvasGroup, float endValue, float duration, TweenCallback onFadeCompleted = null, TweenCallback onFadeStarted = null)
    {
        if (tween is not null)
        {
            tween.Kill(false);
        }

        tween = canvasGroup.DOFade(endValue, duration);

        tween.onPlay += onFadeStarted;
        tween.onComplete += onFadeCompleted;
    }
}
