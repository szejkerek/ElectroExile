using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Guide : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMesh;

    private void OnTriggerEnter2D(Collider2D other)
    {
        textMesh.text = GetComponent<TextMeshProUGUI>().text;
    }

}
