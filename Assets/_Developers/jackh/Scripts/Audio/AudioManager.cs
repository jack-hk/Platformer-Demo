using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

//JackHK, v1 AudioManager

abstract public class AudioManager<T> : MonoBehaviour where T : Audio
{
    private const string VOLUME_CONTROL = "VolumeControl";

    protected static Dictionary<string, float> audioTimerDictionary;

    public T[] audioClips;
    public AudioMixer volumeControl;

    private void Awake()
    {
        audioTimerDictionary = new Dictionary<string, float>();
        Initialize();
    }

    public virtual void Initialize()
    {
        foreach (T audio in audioClips)
        {
            audio.source = gameObject.AddComponent<AudioSource>();
            audio.source.clip = audio.clip;

            audio.source.loop = audio.isLoop;

            if (audio.hasCooldown)
            {
                audioTimerDictionary[audio.name] = 0f;
            }
        }
    }

    #region Playback
    public void Play(string name)
    {
        T audio = Array.Find(audioClips, s => s.name == name);
        if (audio == null)
        {
            Debug.Log("Audio not found: " + name);
            return;
        }
        if (!CanPlayClip(audio)) return;
        audio.source.Play();
    }

    public void Stop(string name)
    {
        T audio = Array.Find(audioClips, s => s.name == name);
        if (audio == null)
        {
            Debug.Log("Audio not found: " + name);
            return;
        }
        audio.source.Stop();
    }

    private static bool CanPlayClip(T clip)
    {
        if (audioTimerDictionary.ContainsKey(clip.name))
        {
            float lastTimePlayed = audioTimerDictionary[clip.name];

            if (lastTimePlayed + clip.clip.length < Time.time)
            {
                audioTimerDictionary[clip.name] = Time.time;
                return true;
            }

            return false;
        }
        return true;
    }
    #endregion
    #region Volume Control
    public void SliderVolume (float sliderValue) 
    {
        volumeControl.SetFloat("VolumeControl", Mathf.Log10(sliderValue) * 20);
    }

    public float storedValue;
    //DEBUGGING LINE
    public void IsVolumeOn(bool isOn)
    { 
        if (!isOn)
        {
            volumeControl.GetFloat(VOLUME_CONTROL, out float oldValue);
            storedValue = oldValue;
            volumeControl.SetFloat(VOLUME_CONTROL, -80);
        }
        else
        {
            volumeControl.SetFloat(VOLUME_CONTROL, storedValue);
        }
    }
    #endregion
}

public class Audio
{ 
    public string name;

    public AudioClip clip;

    public bool isLoop;
    public bool hasCooldown;
    public AudioSource source;
}

