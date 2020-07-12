using System;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public enum CommandType
{
    Move, Rotate, Melee, Laser, Rocket, None
}
public class CommandManager : MonoBehaviour
{
    private bool _left;
    private bool _right;
    private CommandType _type = CommandType.None;
    private Sequence _mySequence;

    private CameraShake _cameraShake;
    private float _trauma = .3f;
    private float _defaultDecay;

    [SerializeField] private int energy;
    [SerializeField] int cost;
    [SerializeField] private Slider energySlider;

    [SerializeField] float rotationSpeed;
    [SerializeField] float jumpDistance;
    [SerializeField] float jumpHeight;
    [SerializeField] float moveDistance;
    [SerializeField] float moveSpeed;
    [SerializeField] float ringLimit;
    [SerializeField] GameObject rocketPrefab;
    [SerializeField] private Transform rocketSpawnPoint;

    private bool _lastMovedLeft;

    public int Energy
    {
        get => energy;
        set
        {
            energy = value;
            if (energy > 1)
            {
                energySlider.value = energy;
                GameManager.Instance.TurnOffRandomButton();
            }

            if (energy < 1)
            {
                GameManager.Instance.EndingSequence();
            }
        }
    }

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

    private void Awake()
    {
        _cameraShake = GetComponentInChildren<CameraShake>();
        _defaultDecay = _cameraShake.Decay;
    }

    [ContextMenu("Execute The thing")]
    public void ExecuteCommand()
    {
        Energy -= cost;
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
        if(_type == CommandType.Rotate)
        {
            RotatePlayer();
            return;
        }
        if (_type == CommandType.Move)
        {
            MovePlayer();
            return;
        }
        if (_type == CommandType.Rocket)
        {
            Rockets();
            return;
        }

    }

    private void Error()
    {
        onError?.Invoke();
    }

    private void Update()
    {
        if (transform.localPosition.x < -8 )
        {
            transform.localPosition = new Vector3(-7, transform.localPosition.y, transform.localPosition.z);
            return;
        }

        if (transform.localPosition.x > ringLimit)
        {
            transform.localPosition = new Vector3(ringLimit - 1, transform.localPosition.y, transform.localPosition.z);
            return;
        }

        if (transform.localPosition.z < -8)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, -7);
            return;
        }

        if (transform.localPosition.z > ringLimit)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, ringLimit - 1);
            return;
        }
    }

    private void MovePlayer()
    {
        
        _mySequence = DOTween.Sequence().PrependInterval(moveSpeed).PrependCallback(CameraJolt);
        if (_left && _right)
        {
            //jump
            JumpForward();
        }
        else if (_lastMovedLeft && !_left)
        {
            //move
            MoveForward();
            _lastMovedLeft = false;
        }
        else if(!_lastMovedLeft && !_right)
        {
           //move again 
           MoveForward();
           _lastMovedLeft = true;
        }
    }

    private void JumpForward()
    {
        CameraSteadyShake();
        SetTraumaDecay(.8f);
        _mySequence.Prepend(transform
            .DOLocalJump(new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z) + transform.forward * jumpDistance,
                jumpHeight, 1, 2f).SetEase(Ease.InSine));
    }

    private void MoveForward()
    {
        SetTraumaAmount(.1f);
        CameraSteadyShake();
        _mySequence.Prepend(transform
            .DOLocalMove(new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z) + transform.forward * moveDistance,
                moveSpeed).SetEase(Ease.InSine));
    }

    private void RotatePlayer()
    {
        SetTraumaAmount(.1f);
        CameraSteadyShake();
        _mySequence = DOTween.Sequence().PrependInterval(rotationSpeed).PrependCallback(CameraJolt);
        if (_left)
        {
            RotateTo(-90);
        }
        else
        {
            RotateTo(90);
        }
        //Rotate by 90
    }

    private void RotateTo(float amount)
    {
        _mySequence.Prepend(transform.DOLocalRotate(new Vector3(transform.localRotation.eulerAngles.x, transform.localRotation.eulerAngles.y + amount, transform.localRotation.eulerAngles.z),
            rotationSpeed).SetEase(Ease.InCubic));
    }
    
    private void Rockets()
    {
        //Fire Homing Rockets
        CameraJolt();
        var rand = Random.Range(0, 3);
        for (int i = 0; i < rand; i++)
        {
            GameObject go = Instantiate(rocketPrefab, rocketSpawnPoint.position + new Vector3(Random.Range(-3,3),Random.Range(-3,3),Random.Range(-3,3)), Quaternion.identity);
        }
    }
    
    public void ResetCommand()
    {
        _left = false;
        _right = false;
        _type = CommandType.None;
    }

    public void SetTraumaAmount(float amount)
    {
        _trauma = amount;
    }
    public void SetTraumaDecay(float amount)
    {
        _cameraShake.Decay = amount;
    }
    private void CameraJolt()
    {
        SetTraumaDecay(.8f);
        SetTraumaAmount(1f);
        _cameraShake.Trauma = _trauma;
        SetTraumaAmount(.2f);
    }

    private void CameraSteadyShake()
    {
        SetTraumaDecay(0);
        _cameraShake.Trauma = _trauma;
    }
}
