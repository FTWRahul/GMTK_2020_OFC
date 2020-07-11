using UnityEngine;

public class TypeCommand : Command
{
    [SerializeField] private CommandType type;

    private bool _isInUse;

    protected override void OnPress()
    {
        _isInUse = !_isInUse;
        if (!_isInUse)
        {
            commandManager.commandType = type;
        }
        else
        {
            commandManager.commandType = CommandType.None;
        }
    }
}
