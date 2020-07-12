using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhichLegToMove : MonoBehaviour
{
    [SerializeField] private MechSoundManager audioSource;

    [SerializeField] private AudioClip _clipLeft;
    [SerializeField] private AudioClip _clipRight;

    private void Awake()
    {
    }

    public void WhichLeg()
    {
        if (GameManager.Instance.commandManager.LastMovedLeft)
        {
            PlayAudio(_clipRight);
        }
        else
        {
            PlayAudio(_clipLeft);
        }
    }

    private void PlayAudio(AudioClip clip)
    {
        audioSource.PlayExtraSounds(clip);
    }
}
