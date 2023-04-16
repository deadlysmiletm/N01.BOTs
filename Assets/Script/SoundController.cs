using UnityEngine;

public class SoundController : MonoBehaviour
{
    public AudioClip musicAudio;
    public bool PlayAtAwake, StopMusicWithTrigger, PlayWithTrigger;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayAtAwake)
            SoundManager.Instance.PlayMusic(musicAudio);
    }

    public void ExecuteBehaviour()
    {
        if (StopMusicWithTrigger)
            SoundManager.Instance.StopMusic();
        else if (PlayWithTrigger)
            SoundManager.Instance.PlayMusic(musicAudio);

        gameObject.SetActive(false);
    }
}
