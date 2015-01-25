using UnityEngine;
using System.Collections;

public class Sfx : MonoBehaviour
{
    public AudioClip[] clips;
    public AudioClip[] pizzicatos;

    public AudioClip[] clips0;
    public AudioClip[] pizz0;

    public AudioClip[] clips1;
    public AudioClip[] pizz1;

    public AudioClip[] clips2;
    public AudioClip[] pizz2;

    public AudioClip[] clips3;
    public AudioClip[] pizz3;

    private int clipId = 0;
    // Use this for initialization
    void Start()
    {

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

    public void SetChords(int chordId)
    {
        if (clips0.Length == 0)
            return;

        switch (chordId)
        {
            default:
            case 0:
                clips = clips0;
                pizzicatos = pizz0;
                break;
            case 1:
                clips = clips1;
                pizzicatos = pizz1;
                break;
            case 2:
                clips = clips2;
                pizzicatos = pizz2;
                break;
            case 3:
                clips = clips3;
                pizzicatos = pizz3;
                break;
        }
    }

}
