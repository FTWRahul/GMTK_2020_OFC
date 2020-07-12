using System;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class ButtonSelectionResponse : MonoBehaviour , ISelectionResponse
{
    public UnityEvent buttonSelected;
    public UnityEvent buttonDeselected;
    public UnityEvent buttonPressed;

    public GameObject activeConfirmation;

    private bool isActive;

    [SerializeField] private TextMeshProUGUI instructionText;
    [SerializeField] private Image instructionIcon;

    public bool IsActive => isActive;

    private void Start()
    {
        GameManager.Instance.flickerIconButtons.Add(this);
    }

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
    }

    public void ResetOtherButtonsOfType()
    {
        GameManager.Instance.ResetTypeButtonsExcept(GetComponent<TypeCommand>().TypeOfCommand);
    }

    public void ClearAllButtons()
    {
        GameManager.Instance.ResetAllButtons();
    }

    [ContextMenu( "Fade it away")]
    public void FadeInstructionIcon()
    {
        Invoke(nameof(DelayedFade), Random.Range(1f,5f));
        
    }

    private void DelayedFade()
    {
        if (instructionText != null)
        {
            instructionText.DOFade(0, Random.Range(1f,2f)).SetEase(Ease.InOutBounce);
        }

        if (instructionIcon != null)
        {
            instructionIcon.DOFade(0, Random.Range(1f,2f)).SetEase(Ease.InOutBounce);
        }
    }
    
}
