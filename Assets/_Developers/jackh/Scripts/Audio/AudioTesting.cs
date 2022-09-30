using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTesting : MonoBehaviour
{
    private GameObject effectManager;
    private GameObject musicManager;

    void Start()
    {
        effectManager = GameObject.Find("Audio/Effect");
        musicManager = GameObject.Find("Audio/Music");

        effectManager.GetComponent<SoundManager>().Play("Door");

        musicManager.GetComponent<TrackManager>().Play("Track_01Drums");
        musicManager.GetComponent<TrackManager>().Play("Track_01Bass");
        musicManager.GetComponent<TrackManager>().Play("Track_01Guitar1");
        musicManager.GetComponent<TrackManager>().Play("Track_01Guitar2");
    }
}
