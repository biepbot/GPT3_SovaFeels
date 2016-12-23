using Assets.Scripts.Base;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndMenuScript : MonoBehaviour {

	public void LoadMainMenu()
    {
        LevelLoader.LoadMainMenu();
    }

    public void LoadStatistics()
    {
        LevelLoader.LoadStatistics();
    }
}
