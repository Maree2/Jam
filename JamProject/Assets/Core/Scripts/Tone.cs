using UnityEngine;
using System.Collections;

public class Tone : MonoBehaviour
{

    void Awake()
    {
        if (audio == null)
            Debug.LogError("No AudioSource");
        if (audio.clip == null)
            Debug.LogError("No clip in AudioSource");
    }

    public void Play()
    {
        audio.Play();
    }

    public void Stop()
    {
        audio.Stop();
    }
}
