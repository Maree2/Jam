using UnityEngine;
using System.Collections;

public class BgmController : MonoBehaviour
{
    public float chordsThreshold = 3f;

    public AudioClip[] chords;

    private int chordIndex = 0;
    private int[] chordsProg;

    private float timer = 0f;

    // Use this for initialization
    void Start()
    {
        chordIndex = 0;
        audio.clip = chords[chordIndex];
        audio.Play();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (chordIndex % 2 == 0)
        {
            if (!audio.isPlaying)
            {
                chordIndex++;
                audio.loop = true;
                audio.clip = chords[chordIndex];
            }
        }

        if (timer >= chordsThreshold)
        {
            timer = 0f;
            if (chordIndex % 2 == 0)
                chordIndex += 2;
            else
                chordIndex++;

            if (chordIndex >= 8)
                chordIndex = 0;
            audio.clip = chords[chordIndex];
            if (chordIndex % 2 == 0)
                audio.loop = false;
            else
                audio.loop = true;
            audio.Play();
        }

    }
}
