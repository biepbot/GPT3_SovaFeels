using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogScript : MonoBehaviour {

    public void PlayClickSound()
    {
        SoundManager.Instance.PlayButtonClickSound();
    }
}
