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
        if (playOnAwake)
        {
            audioSource.clip = clipToPlayOnAwake;
            audioSource.playOnAwake = true;
        }
        else
        {
            audioSource.clip = audioClips[0];
        }

        if (loop) audioSource.loop = loop;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Play()
    {
        audioSource.Play();
    }

    public void Pause()
    {
        audioSource.Pause();
    }

    public void UnPause()
    {
        audioSource.UnPause();
    }

    public void Stop()
    {
        audioSource.Stop();
    }

    public void SelectAudioClip(int audioClip)
    {
        audioSource.clip = audioClips[audioClip];
    }

    public void PlayAudioClip(int audioClip)
    {
        audioSource.PlayOneShot(audioClips[audioClip]);
    }
}
