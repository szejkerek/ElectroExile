using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// <c>ButtonHoverDisabling</c> is deprecated.
/// DO NOT USE!!!
/// </summary>
public class ButtonHoverDisabling : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField] public List<Button> ButtonsToBeDisabled;

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("dupa");
        if (ButtonsToBeDisabled.Count > 0)
        {
            foreach (var button in ButtonsToBeDisabled)
            {
                button.enabled = false;
                Debug.Log($"{button.name} is disabled");
            }
        }
    }
    
    public void OnPointerExit(PointerEventData eventData)
    {
        if (ButtonsToBeDisabled.Count > 0)
        {
            foreach (var button in ButtonsToBeDisabled)
            {
                button.enabled = true;
                Debug.Log($"{button.name} is enabled");
            }
        }
    }

}
