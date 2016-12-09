using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Assets.Scripts.Base;

public class MainMenuScript : MonoBehaviour
{
    public void StartNewPlaythrough()
    {
        //Forces a new playthrough
        LevelLoader.NewPlayThrough(true);
    }

    public void ContinueOldPlaythrough()
    {
        //Loads the old, if any, else, loads a new one
        LevelLoader.LoadPlayThrough(true);
    }

    public void ViewStats()
    {
        //Loads the statistics scene
        SceneManager.LoadScene("StatsScene");
    }
}
