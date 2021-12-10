using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicHandler : MonoBehaviour
{
    public float defaultVolume = 0.33f;
    public AudioClip introSong;
    public AudioClip mainSong;
    public AudioClip defeatSong;
    private AudioSource audioSource;
    private bool musicFadeOutEnabled = false;

    private AudioClip songInQueue;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(introSong);
    }

    // Update is called once per frame
    void Update()
    {
        if (musicFadeOutEnabled)
        {
            if (audioSource.volume <= 0.01f || !audioSource.isPlaying)
            {
                audioSource.Stop();
                audioSource.volume = defaultVolume;
                musicFadeOutEnabled = false;
                PlaySongInQueue();
            }
            else
            {
                float newVolume = audioSource.volume - (0.1f * Time.deltaTime);
                if (newVolume < 0f)
                {
                    newVolume = 0f;
                }
                audioSource.volume = newVolume;
            }
        }
    }

    private void PlaySongInQueue()
    {
        audioSource.PlayOneShot(songInQueue);
    }

    public void PlayMainSong()
    {
        musicFadeOutEnabled = true;
        songInQueue = mainSong;
    }

    public void PlayDefeatSong()
    {
        StopMusic();
        audioSource.PlayOneShot(defeatSong);
    }

    public void StopMusic()
    {
        audioSource.Stop();
    }
}
