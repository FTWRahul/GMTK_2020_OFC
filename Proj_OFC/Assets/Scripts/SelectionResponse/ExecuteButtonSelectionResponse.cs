using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExecuteButtonSelectionResponse : ButtonSelectionResponse
{
    public override void OnPress()
    {
        base.OnPress();
        ClearAllButtons();
    }

    public void ExecuteCommand()
    {
        GameManager.Instance.commandManager.ExecuteCommand();
    }
}
