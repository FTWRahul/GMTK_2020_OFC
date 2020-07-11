using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TypeButtonSelectionResponse : ButtonSelectionResponse
{
    public override void OnPress()
    {
        base.OnPress();
        ResetOtherButtonsOfType();
    }
}
