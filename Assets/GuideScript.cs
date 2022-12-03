using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GuideScript : MonoBehaviour
{
    [SerializeField] private TextMeshPro text;
    [SerializeField] private float duration = 2f;
    [SerializeField] private float pause = 5f;

    private void Start()
    {
        //text.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        text.enabled = true;
    }

 
}
