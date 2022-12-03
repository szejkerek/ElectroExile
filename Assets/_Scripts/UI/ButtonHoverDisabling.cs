using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonHoverDisabling : MonoBehaviour
{
    [SerializeField] public List<Button> ButtonsToBeDisabled;

    private void OnMouseOver()
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
    
    private void OnMouseExit()
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
