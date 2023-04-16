using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource MusicSource;

    public static SoundManager Instance;

    private void Awake()
    {
        if (Instance != null)
            Destroy(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        Instance = this;
    }

    public void PlayMusic(AudioClip musicAudio)
    {
        if (MusicSource.isPlaying)
        {
            StopAllCoroutines();
            StartCoroutine(FadeOutAndIn(MusicSource, musicAudio, 1.5f));
        }
        else
        {
            MusicSource.clip = musicAudio;
            MusicSource.Play();
        }
    }

    public void StopMusic()
    {
        StartCoroutine(FadeOut(MusicSource));
    }

    private IEnumerator FadeOut(AudioSource source)
    {
        float lerpValue = 0;
        float result = 0;

        yield return new WaitUntil(() =>
        {
            source.volume = Mathf.Lerp(source.volume, 0, result);
            lerpValue += Time.deltaTime /2;
            result = lerpValue / 200;
            return result > 1;
        });

        source.Stop();
        source.volume = 1;
    }

    private IEnumerator FadeOutAndIn(AudioSource source, AudioClip newClip, float duration)
    {
        float lerpValue = 0;
        float result = 0;
        float midDuration = duration / 2;

        yield return new WaitUntil(() =>
        {
            source.volume = Mathf.Lerp(source.volume, 0, result);

            lerpValue += Time.deltaTime;
            result = lerpValue / midDuration;

            return result > 1;
        });

        source.Stop();
        lerpValue = 0;
        result = 0;

        source.clip = newClip;
        source.Play();

        yield return new WaitUntil(() =>
        {
            source.volume = Mathf.Lerp(0, 1, result);

            lerpValue += Time.deltaTime;
            result = lerpValue / midDuration;

            return result > 1;
        });
    }
}
