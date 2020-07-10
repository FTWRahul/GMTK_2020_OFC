using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonSelectionResponse : MonoBehaviour , ISelectionResponse
{
    public UnityEvent buttonSelected;
    public UnityEvent buttonDeselected;
    public UnityEvent buttonPressed;
    public void OnSelect()
    {
        buttonSelected?.Invoke();
    }
    public void OnDeselect()
    {
        buttonDeselected?.Invoke();
    }
    public void OnPress()
    {
        buttonPressed?.Invoke();
    }
}
