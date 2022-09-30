using System;
using UnityEngine;
using UnityEngine.Audio;

//JackHK

public class SoundManager : AudioManager<Sound>
{
    public AudioMixerGroup mixer;
    public override void Initialize()
    {
        foreach (Sound audio in audioClips)
        {
            audio.source = gameObject.AddComponent<AudioSource>();
            audio.source.clip = audio.clip;
            audio.source.outputAudioMixerGroup = mixer;

            audio.source.volume = audio.volume;
            audio.source.pitch = audio.pitch;

            audio.source.loop = audio.isLoop;

            if (audio.hasCooldown)
            {
                audioTimerDictionary[audio.name] = 0f;
            }
        }
    }
}

[Serializable]
public class Sound : Audio
{
    [Range(0f, 1f)]
    public float volume = 1f;

    [Range(.1f, 3f)]
    public float pitch = 1f;
}
