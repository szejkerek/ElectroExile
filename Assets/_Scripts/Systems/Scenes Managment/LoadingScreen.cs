using DG.Tweening;
using System.Collections;
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

    Sequence seq;
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

    public float FadeIn(bool showProgressBar = true)
    {
        seq = DOTween.Sequence();
        progressBarCG.alpha = 0;
        seq.Append(loadingScreenCG.DOFade(1, fadeInDuration));
        seq.AppendInterval(progressBarDelay);
        seq.Append(progressBarCG.DOFade(1, fadeInProgressBarDuration));
        return fadeInDuration;
    }

    public void FadeOut()
    {
        seq.Kill();
        seq = DOTween.Sequence();
        seq.Append(progressBarCG.DOFade(0, fadeOutProgressBarDuration));
        seq.Append(loadingScreenCG.DOFade(0, fadeOutDuration));
    }


    private void Fade(Sequence tween, CanvasGroup canvasGroup, float endValue, float duration, float delay = 0f)
    {   
        tween.Kill(false);
        tween = DOTween.Sequence();
        tween.AppendInterval(delay);
        tween.Append(canvasGroup.DOFade(endValue, duration));
    }
}
