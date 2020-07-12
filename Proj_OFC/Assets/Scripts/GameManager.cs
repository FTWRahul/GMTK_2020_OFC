using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public static GameManager Instance => _instance;

    public CommandManager commandManager;

    public float mouseSensitivity = 50;

    public List<ButtonSelectionResponse> typeButtons;
    public List<ButtonSelectionResponse> otherButtons;
    public List<ButtonSelectionResponse> flickerIconButtons;

    public List<ITakeHit> damageAbles = new List<ITakeHit>();

    private CameraShake _cameraShake;

    private bool _isEnded;

    public CameraShake CameraShake
    {
        get => _cameraShake;
    }

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
        _cameraShake = commandManager.GetComponentInChildren<CameraShake>();
    }

    private void Start()
    {
        StartingSequence();
    }

    public void ResetTypeButtonsExcept(CommandType thisOne)
    {
        foreach (var btn in typeButtons)
        {
            var typeScript = btn.GetComponent<TypeCommand>();
            if (typeScript.TypeOfCommand != thisOne && btn.IsActive)
            {
                btn.FlipActiveStatus();
                typeScript.IsInUse = false;
            }
        }
    }

    public void ResetAllButtons()
    {
        foreach (var btn in typeButtons)
        {
            if (btn.IsActive)
            {
                btn.FlipActiveStatus();
                btn.GetComponent<TypeCommand>().IsInUse = false;
            }
        }
        foreach (var btn in otherButtons)
        {
            if (btn.IsActive)
            {
                btn.FlipActiveStatus();
            }
        }
        commandManager.ResetCommand();
    }

    public void TurnOffRandomButton()
    {
        var rand = Random.Range(0, flickerIconButtons.Count);
        flickerIconButtons[rand].FadeInstructionIcon();
    }

    public void StartingSequence()
    {
        StartCoroutine(StartingEnumarator());
    }

    private IEnumerator StartingEnumarator()
    {
        _cameraShake.Trauma = .5f;
        _cameraShake.Decay = 0;
        yield return new WaitForSeconds(1f);
        _cameraShake.Decay = .8f;
        commandManager.GetComponent<MechSoundManager>().PlayIntro();
    }

    [ContextMenu("Test End Sequence")]
    public void EndingSequence()
    {
        if (_isEnded)
        {
            return;
        }
        commandManager.GetComponent<MechSoundManager>().PlayEnd();
        StartCoroutine(EndingEnumerator());
        _isEnded = true;

    }

    private IEnumerator EndingEnumerator()
    {
        yield return new WaitForSeconds(9f);
        commandManager.ReleaseSelfDestructButton();
        yield return new WaitForSeconds(35f);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SceneManager.LoadScene("End_Scene");
    }


    
}
