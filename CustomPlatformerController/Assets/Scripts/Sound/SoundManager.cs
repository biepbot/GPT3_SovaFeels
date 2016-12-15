using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{

    public AudioMixer audioMixer;

#if UNITY_EDITOR
    [Range(0, 100)]
    public int master = 80;

    [Range(0, 100)]
    public int menu = 80;

    [Range(0, 100)]
    public int music = 80;

    [Range(0, 100)]
    public int game = 80;

    [Range(0, 100)]
    public int ambient = 80;

    [Range(0, 100)]
    public int helper = 80;

    [Range(0, 100)]
    public int player = 80;

    [Range(0, 100)]
    public int characters = 80;
#endif

    private ObjectSound[] objectSounds;

    // Use this for initialization
    void Start()
    {
        objectSounds = FindObjectsOfType<ObjectSound>();
    }
    

    private void FixedUpdate()
    {
#if UNITY_EDITOR
        SetAudio();
#endif
    }

#if UNITY_EDITOR
    private void SetAudio()
    {
        SetVolumeOnDifference(AudioParamater.Ambient, ambient);
        SetVolumeOnDifference(AudioParamater.Characters, characters);
        SetVolumeOnDifference(AudioParamater.Game, game);
        SetVolumeOnDifference(AudioParamater.Helper, helper);
        SetVolumeOnDifference(AudioParamater.Master, master);
        SetVolumeOnDifference(AudioParamater.Menu, menu);
        SetVolumeOnDifference(AudioParamater.Music, music);
        SetVolumeOnDifference(AudioParamater.Player, player);
    }
#endif

    /// <summary>
    /// Plays all audiosources.
    /// </summary>
    public void Play()
    {
        foreach (ObjectSound objSound in objectSounds)
        {
            objSound.Play();
        }
    }

    /// <summary>
    /// Plays all audiosources from the given channel.
    /// </summary>
    /// <param name="ap">The audiochannel</param>
    public void Play(AudioParamater ap)
    {
        foreach (ObjectSound objSound in objectSounds)
        {
            if (objSound.audioChannel == ap)
            {
                objSound.Play();
            }
        }
    }

    /// <summary>
    /// Stops all audiosources
    /// </summary>
    public void Stop()
    {
        foreach (ObjectSound objSound in objectSounds)
        {
            objSound.Stop();
        }
    }

    /// <summary>
    /// Stops all audiosources
    /// </summary>
    /// <param name="ap">The audiochannel</param>
    public void Stop(AudioParamater ap)
    {
        foreach (ObjectSound objSound in objectSounds)
        {
            if (objSound.audioChannel == ap)
            {
                objSound.Stop();
            }
        }
    }

    /// <summary>
    /// Pauses all audiosources
    /// </summary>
    public void Pause()
    {
        foreach (ObjectSound objSound in objectSounds)
        {
            objSound.Pause();
        }
    }

    /// <summary>
    /// Pauses all audiosources
    /// </summary>
    /// <param name="ap">The audiochannel</param>
    public void Pause(AudioParamater ap)
    {
        foreach (ObjectSound objSound in objectSounds)
        {
            if (objSound.audioChannel == ap)
            {
                objSound.Pause();
            }
        }
    }

    /// <summary>
    /// UnPauses all audiosources
    /// </summary>
    public void UnPause()
    {
        foreach (ObjectSound objSound in objectSounds)
        {
            objSound.UnPause();
        }
    }

    /// <summary>
    /// UnPauses all audiosources
    /// </summary>
    /// <param name="ap">The audiochannel</param>
    public void UnPause(AudioParamater ap)
    {
        foreach (ObjectSound objSound in objectSounds)
        {
            if (objSound.audioChannel == ap)
            {
                objSound.UnPause();
            }
        }
    }

    /// <summary>
    /// Gets the volume of the given channel.
    /// </summary>
    /// <param name="ap">The audio channel you want to know the volume of</param>
    /// <returns>The currentlevel of the volume</returns>
    public int GetVolume(AudioParamater ap)
    {
        float apVolume;
        audioMixer.GetFloat(Volume.GetVolumeParamater(ap), out apVolume);
        Debug.Log("Audio for " + Volume.GetVolumeParamater(ap) + " is " + apVolume);
        return (int)apVolume;
    }

    /// <summary>
    /// Checks for the current volume and changes it if its different with the volume paramater.
    /// </summary>
    /// <param name="ap">The audio channel you want to check for a volume change</param>
    /// <param name="volume">The volume you want to check on</param>
    /// <returns>The current volume</returns>
    public int SetVolumeOnDifference(AudioParamater ap, int volume = 80)
    {
        if (volume < 0 || volume > 100) throw new System.Exception("The volume value cannot be lower than 0 or higher than 100");

        float mixerVolume;
        audioMixer.GetFloat(Volume.GetVolumeParamater(ap), out mixerVolume);

        if ((volume - 80) != (int)mixerVolume)
        {
            SetVolume(ap, volume);
            return volume;
        }
        else
        {
            return volume;
        }
    }

    /// <summary>
    /// Sets the volume of a certain audio channel.
    /// </summary>
    /// <param name="ap">The audio channel you want to change</param>
    /// <param name="volume">The volume of the audiochannel</param>
    public void SetVolume(AudioParamater ap, int volume = 80)
    {
        if (volume < 0 || volume > 100) throw new System.Exception("The volume value cannot be lower than 0 or higher than 100");

        float apVolume = (float)(volume - 80);
        Debug.Log("Setting volume to:" + apVolume + " on " + Volume.GetVolumeParamater(ap));
        audioMixer.SetFloat(Volume.GetVolumeParamater(ap), apVolume);
    }
}

public static class Volume
{
    public const string MASTER = "masterVolume";
    public const string MENU = "menuVolume";
    public const string MUSIC = "musicVolume";
    public const string GAME = "gameVolume";
    public const string AMBIENT = "ambientVolume";
    public const string HELPER = "helperVolume";
    public const string PLAYER = "playerVolume";
    public const string CHARACTERS = "characterVolume";

    public static string GetVolumeParamater(AudioParamater ap)
    {
        switch (ap)
        {
            case AudioParamater.Ambient:
                return AMBIENT;
            case AudioParamater.Characters:
                return CHARACTERS;
            case AudioParamater.Game:
                return GAME;
            case AudioParamater.Helper:
                return HELPER;
            case AudioParamater.Master:
                return MASTER;
            case AudioParamater.Menu:
                return MENU;
            case AudioParamater.Music:
                return MUSIC;
            case AudioParamater.Player:
                return PLAYER;
            default:
                return null;
        }
    }
}

public enum AudioParamater
{
    Master,
    Menu,
    Music,
    Game,
    Ambient,
    Helper,
    Player,
    Characters
}