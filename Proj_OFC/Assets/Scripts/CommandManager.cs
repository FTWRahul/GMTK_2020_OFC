using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum CommandType
{
    Move, Rotate, Melee, Laser, Rocket, None
}
public class CommandManager : MonoBehaviour
{
    private bool _left;
    private bool _right;
    private CommandType _type;
    
    public bool Left
    {
        get => _left;
        set => _left = value;
    }
    
    public bool Right
    {
        get => _right;
        set => _right = value;
    }

    public CommandType commandType
    {
        get => _type;
        set => _type = value;
    }
    
    [SerializeField] private UnityEvent onError;

    public void ExecuteCommand()
    {
        if (!_left && !_right)
        {
            Error();
            return;
        }
        if (_type == CommandType.None)
        {
            Error();
            return;
        }
        if (_left && _right && (_type == CommandType.Rotate))
        {
            Error();
            return;
        }
        
    }

    private void Error()
    {
        onError?.Invoke();
    }

    private void MovePlayer()
    {
        //move corresponding leg
    }
    private void RotatePlayer()
    {
        //Rotate by 90
    }

    private void Melee()
    {
        //Punch with fists
    }

    private void Rockets()
    {
        //Fire Homing Rockets
    }

    private void Laser()
    {
        //Fire Laser
    }

    public void ResetCommand()
    {
        _left = false;
        _right = false;
        _type = CommandType.None;
    }
}
