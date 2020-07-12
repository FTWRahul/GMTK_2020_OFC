using System;
using UnityEngine;

public class TypeCommand : Command
{
    [SerializeField] private CommandType type;
    
    public CommandType TypeOfCommand => type;

    private bool _isInUse;
    
    public bool IsInUse
    {
        get => _isInUse;
        set => _isInUse = value;
    }

    private ButtonSelectionResponse _response;
    private void Start()
    {
        _response = GetComponent<ButtonSelectionResponse>();
        GameManager.Instance.typeButtons.Add(_response);
    }

    public override void OnPress()
    {
        if (!_isInUse)
        {
            commandManager.commandType = type;
        }
        else
        {
            commandManager.commandType = CommandType.None;
        }
        _isInUse = !_isInUse;

    }
}
