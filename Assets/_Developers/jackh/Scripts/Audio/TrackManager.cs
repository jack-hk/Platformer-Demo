using System;
using UnityEngine;
using UnityEngine.Audio;

//JackHK

public class TrackManager : AudioManager<Track>
{
    public override void Initialize()
    {
        foreach (Track audio in audioClips)
        {
            audio.source = gameObject.AddComponent<AudioSource>();
            audio.source.clip = audio.clip;
            audio.source.outputAudioMixerGroup = audio.channel;

            audio.source.loop = audio.isLoop;

            if (audio.hasCooldown)
            {
                audioTimerDictionary[audio.name] = 0f;
            }
        }
    }
}

[Serializable]
public class Track : Audio
{
    public AudioMixerGroup channel;
}