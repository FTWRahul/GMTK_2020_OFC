using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public static GameManager Instance => _instance;

    public CommandManager commandManager;

    public float mouseSensitivity = 50;

    public List<ButtonSelectionResponse> typeButtons;
    public List<ButtonSelectionResponse> otherButtons;
    
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(this);
        }
        DontDestroyOnLoad(gameObject);
        commandManager = FindObjectOfType<CommandManager>();
    }

    public void ResetTypeButtonsExcept(ButtonSelectionResponse thisOne)
    {
        foreach (var btn in typeButtons)
        {
            if (btn != thisOne && btn.IsActive)
            {
                btn.FlipActiveStatus();
                btn.GetComponent<TypeCommand>().IsInUse = false;
            }
        }
    }

    public void ResetAllButtons()
    {
        foreach (var btn in typeButtons)
        {
            if (btn.IsActive)
            {
                btn.OnPress();
            }
        }
        foreach (var btn in otherButtons)
        {
            if (btn.IsActive)
            {
                btn.OnPress();
            }
        }
    }
}
