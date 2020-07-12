using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhichLegToMove : MonoBehaviour
{
    private AudioSource _audioSource;

    [SerializeField] private AudioClip _clipLeft;
    [SerializeField] private AudioClip _clipRight;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
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
        _audioSource.clip = clip;
        _audioSource.Play();
    }
}
