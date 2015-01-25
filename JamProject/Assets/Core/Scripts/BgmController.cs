using UnityEngine;
using System.Collections;

public class BgmController : MonoBehaviour
{
    public float chordLength = 1f;
    public float overlapTime = 0.3f;
    public float attackTime = 0.7f;

    public Chord[] chords;

    public SfxController sfxController;

    private int chordIndex = 0;
    private int chordPrev;

    private float timer;

    // Use this for initialization
    void Start()
    {
        timer = 0f;
        chordIndex = 0;
        chordPrev = 3;
        StartCoroutine(chords[0].Play(attackTime));
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= chordLength)
        {
            timer = 0f;
            chordPrev = chordIndex;
            chordIndex++;
            if (chordIndex >= 4)
            {
                chordIndex = 0;
                chordPrev = 3;
            }
            Chord chordNew = chords[chordIndex];
            Chord chordOld = chords[chordPrev];
            StartCoroutine(chordNew.Play(attackTime));
            StartCoroutine(chordOld.Stop(overlapTime));
            sfxController.SetChord(chordIndex);
        }
    }

}
