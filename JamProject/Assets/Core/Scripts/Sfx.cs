using UnityEngine;
using System.Collections;

public class Sfx : MonoBehaviour
{
    public AudioClip[] clips;
    public AudioClip[] pizzicatos;
    private int clipId = 0;

    // Use this for initialization
    void Start()
    {
        audio.clip = clips[clipId];
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Play()
    {
        clipId = 0;
        if (clips.Length > 1)
            clipId = Random.Range(0, clips.Length - 1);
        audio.clip = clips[clipId];
        audio.loop = true;
        audio.Play();
    }

    public void Stop()
    {
        audio.Stop();
    }

    public void Pizzicato()
    {
        clipId = 0;
        if (clips.Length > 1)
            clipId = Random.Range(0, pizzicatos.Length - 1);
        audio.clip = pizzicatos[clipId];
        audio.loop = false;
        audio.Play();
    }

}
