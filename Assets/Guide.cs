using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Guide : MonoBehaviour
{
    [SerializeField] private float durationOfText = 2;
    [SerializeField] private TextMeshProUGUI textMesh;

    bool displayedOnce = false;

    private void Awake()
    {
        textMesh.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(!displayedOnce)
        {
            textMesh.enabled = true;
            textMesh.color = new Color(textMesh.color.r, textMesh.color.g, textMesh.color.b, 0);

            StartCoroutine(Animation());
            displayedOnce = true;
        }

    }

    private IEnumerator Animation()
    {
        float currentTime = 0f;
        while (currentTime < durationOfText)
        {
            float alpha = Mathf.Lerp(0f, 1f, currentTime / durationOfText);
            textMesh.color = new Color(textMesh.color.r, textMesh.color.g, textMesh.color.b, alpha);
            currentTime += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(2f);

        currentTime = 0f;
        while (currentTime < durationOfText)
        {
            float alpha = Mathf.Lerp(1f, 0f, currentTime / durationOfText);
            textMesh.color = new Color(textMesh.color.r, textMesh.color.g, textMesh.color.b, alpha);
            currentTime += Time.deltaTime;
            yield return null;
        }
        textMesh.enabled = false;
        yield break;
    }
}
