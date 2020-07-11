using System;
using UnityEngine;

public class DirectionCommand : Command
{
    [SerializeField] private bool left;
    [SerializeField] private bool right;


    private ButtonSelectionResponse _response;

    private void Start()
    {
        _response = GetComponent<ButtonSelectionResponse>();
        GameManager.Instance.otherButtons.Add(_response);
    }

    public override void OnPress()
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
