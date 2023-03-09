using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class BasicLoadingScreen : MonoBehaviour
{
    private Slider progressBar;
    private CanvasGroup loadingScreenCG;
    private CanvasGroup progressBarCG;

    private Tween loadingScreenTween;
    private Tween progressBarTween;

    public float ProgressBarValue 
    {
      get { return progressBar.value; }
      set { progressBar.value = value; } 
    }

    [SerializeField] private float fadeInDuration = 3f;
    [SerializeField] private float fadeOutDuration = 10f;

    [Header("Progress Bar")]
    [SerializeField] private float progressBarDelay = 2f;
    [SerializeField] private float fadeInProgressBarDuration = 1f;
    [SerializeField] private float fadeOutProgressBarDuration = 0.25f;

    private void Awake()
    {
        progressBar = GetComponentInChildren<Slider>();
        loadingScreenCG = GetComponent<CanvasGroup>();
        progressBarCG = progressBar.GetComponent<CanvasGroup>();
     }

    public void FadeIn()
    {
        Fade(loadingScreenTween, loadingScreenCG, 1, fadeInDuration);
        StartCoroutine(FadeInProgressBar());
    }

    public void FadeOut()
    {
        FadeOutProgressBar();
        Fade(loadingScreenTween, loadingScreenCG, 0, fadeOutDuration);
    }

    private IEnumerator FadeInProgressBar()
    {
        yield return new WaitForSeconds(fadeInDuration + progressBarDelay);
        Fade(progressBarTween, progressBarCG, 1, fadeInProgressBarDuration);
    }

    private void FadeOutProgressBar()
    {
        Fade(progressBarTween, progressBarCG, 0, fadeOutProgressBarDuration);
    }

    private void Fade(Tween tween, CanvasGroup canvasGroup, float endValue, float duration, TweenCallback onFadeCompleted = null, TweenCallback onFadeStarted = null)
    {
        if(tween is not null)
        {
            tween.Kill(false);
        }

        tween = canvasGroup.DOFade(endValue, duration);

        tween.onPlay += onFadeStarted;
        tween.onComplete += onFadeCompleted;
    }
}
