using UnityEngine;

public class DirectionCommand : Command
{
    [SerializeField] private bool left;
    [SerializeField] private bool right;
    
    protected override void OnPress()
    {
        if (left)
        {
            commandManager.Left = !commandManager.Left;
        }

        if (right)
        {
            commandManager.Right = !commandManager.Right;
        }
    }
}
