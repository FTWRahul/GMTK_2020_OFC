using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;
using Object = UnityEngine.Object;


public class MechSoundManager : MonoBehaviour
{
    public AudioSource source;
    
    private AudioClip _introLines;
    private AudioClip _endLines;
    private List<Object> _randomQuips;
    private List<Object> _errorQuips;
    private List<Object> _nassicStory;
    private int _storyIndex = 0;

    private bool _isEnding;
    private void Awake()
    {
        _randomQuips = Resources.LoadAll("Sounds/Quips", typeof(AudioClip)).ToList();
        _errorQuips = Resources.LoadAll("Sounds/Errors", typeof(AudioClip)).ToList();
        _nassicStory = Resources.LoadAll("Sounds/Nasic", typeof(AudioClip)).ToList();
        _introLines = (AudioClip)Resources.Load("Sounds/IntroFolder/Intro");
        _endLines = (AudioClip)Resources.Load("Sounds/EndFolder/End");
    }

    [ContextMenu("Quip Test")]
    public void PlayRandomQuip()
    {
        if (_randomQuips.Count < 1)
        {
            return;
        }
        var rand = Random.Range(0, _randomQuips.Count);
        AudioClip audioClip = (AudioClip) _randomQuips[rand];
        PlayAudio(audioClip);
        _randomQuips.RemoveAt(rand);
    }
    
    [ContextMenu("Error Test")]
    public void PlayRandomError()
    {
        if (_errorQuips.Count < 1)
        {
            return;
        }
        var rand = Random.Range(0, _errorQuips.Count);
        AudioClip audioClip = (AudioClip) _errorQuips[rand];
        PlayAudio(audioClip);
        _errorQuips.RemoveAt(rand);
    }
    private void PlayAudio(AudioClip audioClip)
    {
        if (_isEnding)
        {
            return;
        }
        source.clip = audioClip;
        source.Play();
    }
    [ContextMenu("Intro Test")]
    public void PlayIntro()
    {
        PlayAudio(_introLines);
    }
    [ContextMenu("End Test")]
    public void PlayEnd()
    {
        PlayAudio(_endLines);
        _isEnding = true;
    }

    public void PlayNasicStory()
    {
        PlayAudio((AudioClip) _nassicStory[_storyIndex]);
        if (_storyIndex < _nassicStory.Count - 1)
        {
            _storyIndex++;
        }
        else
        {
            _storyIndex = 0;
        }
    }

    public void PlayExtraSounds(AudioClip clip)
    {
        if (source.isPlaying)
        {
            return;
        }

        source.clip = clip;
        source.Play();
    }


    
}
