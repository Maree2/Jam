using UnityEngine;
using System.Collections;

public class Sfx : MonoBehaviour
{
    public AudioClip clip;
    // Use this for initialization
    void Start()
    {
        if (clip == null)
            Debug.LogError("No clip assigned");
        audio.clip = clip;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Play()
    {

    }

    void Stop()
    {
        audio.Stop();
    }
}
