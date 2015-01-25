using UnityEngine;
using System.Collections;

public class Sfx : MonoBehaviour
{
    public bool isPercussion;
    public AudioClip clip;
    public AudioClip pizzicato;
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

    public void Play()
    {
        audio.Play();
    }

    public void Stop()
    {
        audio.Stop();
    }

    public void PlayPizzicato()
    {
        audio.clip = pizzicato;
        audio.loop = false;
        audio.Play();
    }

}
