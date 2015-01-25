using UnityEngine;
using System.Collections;

public class Chord : MonoBehaviour
{
    public Tone attack;
    public Tone hold;
    
    private bool isPlaying;
    private bool isStopped;
    private float timerSwap;
    private float timerStop;

    public IEnumerator Play(float swapTime)
    {
        Debug.Log(gameObject.name);
        attack.Play();
        yield return new WaitForSeconds(swapTime);
        //attack.Stop();
        //hold.Play();
        yield break;
    }

    public IEnumerator Stop(float stopTime)
    {
        yield return new WaitForSeconds(stopTime);
        attack.Stop();
        //hold.Stop();
    }
}
