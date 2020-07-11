using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonSelectionResponse : MonoBehaviour , ISelectionResponse
{
    public UnityEvent buttonSelected;
    public UnityEvent buttonDeselected;
    public UnityEvent buttonPressed;

    public GameObject activeConfirmation;

    private bool isActive;

    public bool IsActive => isActive;

    public void FlipActiveStatus()
    {
        isActive = !isActive;
        activeConfirmation.SetActive(isActive);
    }

    public void OnSelect()
    {
        buttonSelected?.Invoke();
    }
    public void OnDeselect()
    {
        buttonDeselected?.Invoke();
    }
    public virtual void OnPress()
    {
        buttonPressed?.Invoke();
        //ResetOtherButtonsOfType();
    }

    public void ResetOtherButtonsOfType()
    {
        GameManager.Instance.ResetTypeButtonsExcept(this);
    }
    
}
