using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSound : MonoBehaviour {

    public AudioParamater audioChannel = AudioParamater.Master;
    public AudioClip[] audioClips;
    public AudioSource audioSource;
    public bool loop;
    public bool playOnAwake;
    public AudioClip clipToPlayOnAwake;

	// Use this for initialization
	void Start () {
        if (loop) audioSource.loop = loop;

        if (playOnAwake)
        {
            audioSource.clip = clipToPlayOnAwake;
            audioSource.playOnAwake = true;
            audioSource.Play();
        }
        else
        {
            audioSource.clip = audioClips[0];
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    /// <summary>
    /// Plays the audiosource.
    /// </summary>
    public void Play()
    {
        audioSource.Play();
    }

    /// <summary>
    /// Pauses the audioSource.
    /// </summary>
    public void Pause()
    {
        audioSource.Pause();
    }

    /// <summary>
    /// Resumes the audiosource.
    /// </summary>
    public void UnPause()
    {
        audioSource.UnPause();
    }

    /// <summary>
    /// Stops the audiosource
    /// </summary>
    public void Stop()
    {
        audioSource.Stop();
    }

    /// <summary>
    /// Selects a audioclip from the array with audioclips;
    /// </summary>
    /// <param name="audioClip">The audioclip you want to pick.</param>
    public void SelectAudioClip(int audioClip)
    {
        audioSource.clip = audioClips[audioClip];
    }

    /// <summary>
    /// Plays a audioclip from the array with audioclips based on index.
    /// </summary>
    /// <param name="audioClip">The audioclip you want to play</param>
    public void PlayAudioClip(int audioClip)
    {
        audioSource.PlayOneShot(audioClips[audioClip]);
    }

    /// <summary>
    /// Plays a random audioclip from the objects audioclips.
    /// </summary>
    public void PlayRandomAudioClip()
    {
        audioSource.PlayOneShot(audioClips[Random.Range(0, audioClips.Length)]);
    }

    /// <summary>
    /// Selects a random audioclip from the objects audioclips.
    /// </summary>
    public void SelectRandomAudioClip()
    {
        audioSource.clip = audioClips[Random.Range(0, audioClips.Length)];
    }
}
