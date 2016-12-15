using Assets.Scripts.Base;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitController : MonoBehaviour {
    public void NextLevel()
    {
        if (LevelLoader.HasMoreLevels)
        {
            LevelLoader.LoadNextLevel();
        }
        else
        {
            Debug.LogWarning("Game finished. All " + LevelLoader.DEFAULT_LEVEL_AMOUNT + " levels were finished\r\nUh. Do something now");
            //TODO
            //Code for completing the game.
        }
    }
}
