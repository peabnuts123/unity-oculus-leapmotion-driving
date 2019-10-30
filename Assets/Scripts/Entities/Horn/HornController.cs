using System.Collections;
using Leap.Unity.Interaction;
using UnityEngine;

public class HornController : MonoBehaviour
{
    // Enums
    enum SoundState
    {
        Idle,
        PlayingStart,
        PlayingEnd,
    }

    // Public references
    [NotNull]
    public InteractionBehaviour interactionBehaviour;
    [NotNull]
    public AudioSource audioSource;

    // Public config
    [NotNull]
    public AudioClip honkStartClip;
    [NotNull]
    public AudioClip honkEndClip;

    // Private state
    private SoundState soundState = SoundState.Idle;
    private Coroutine soundCoroutine;


    public void Start()
    {
        this.interactionBehaviour.OnPerControllerContactBegin += BeginTouchHorn;
        this.interactionBehaviour.OnPerControllerContactEnd += EndTouchHorn;
    }


    IEnumerator PlayHonk()
    {
        this.PlayStartClip();
        yield return new WaitForSeconds(this.honkStartClip.length);

        if (this.IsPlayingStart)
        {
            yield return StartCoroutine(StopHonk());
        }
    }

    IEnumerator StopHonk()
    {
        this.PlayEndClip();
        yield return new WaitForSeconds(this.honkEndClip.length);

        if (this.IsPlayingEnd)
        {
            this.soundState = SoundState.Idle;
        }
    }

    public void BeginTouchHorn(InteractionController controller)
    {
        PlaySoundCoroutine(PlayHonk());
    }

    public void EndTouchHorn(InteractionController controller)
    {
        PlaySoundCoroutine(StopHonk());
    }

    private void PlaySoundCoroutine(IEnumerator coroutine)
    {
        if (this.soundCoroutine != null)
        {
            StopCoroutine(this.soundCoroutine);
        }
        this.soundCoroutine = StartCoroutine(coroutine);
    }

    public void PlayStartClip()
    {
        this.soundState = SoundState.PlayingStart;
        this.audioSource.Stop();
        this.audioSource.PlayOneShot(this.honkStartClip);
    }

    public void PlayEndClip()
    {
        this.soundState = SoundState.PlayingEnd;
        this.audioSource.Stop();
        this.audioSource.PlayOneShot(this.honkEndClip);
    }

    private bool IsPlayingStart
    {
        get { return this.soundState == SoundState.PlayingStart; }
    }
    private bool IsPlayingEnd
    {
        get { return this.soundState == SoundState.PlayingEnd; }
    }
}
