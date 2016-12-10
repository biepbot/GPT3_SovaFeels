using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Assets.Scripts.Base;
using Random = UnityEngine.Random;

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
        LevelLoader.LoadStatistics();
        SaveStats(); // Delete this later on
    }

    // Delete this later on. Temporary fields used for creating a stats file.
    private static SaveSystem saveSystem = new SaveSystem();
    private static bool locking = false;

    // Delete this later on. Temporary method to create stats file with dummy data.
    private void SaveStats()
    {
        if (locking) return;

        locking = true;
        saveSystem.Clear();
        Debug.Log("Saving dummy data to stats file");
        List<Stats> saveData = new List<Stats>();

        saveData.Add(new Stats()
        {
            levelDifficulty = Random.Range(1, 4),
            coins = Random.Range(15, 67),
            amountOfPlaythroughs = Random.Range(9, 21),
            lastFinishedPlaythrough = DateTime.Now
        });

        for (int i = 0; i < 10; i++)
        {
            saveData.Add(new Stats()
            {
                categoryName = "Nee zeggen",
                fight = Random.Range(1, 8),
                handle = Random.Range(1, 8),
                hide = Random.Range(1, 8)
            });

            saveData.Add(new Stats()
            {
                categoryName = "In je recht staan",
                fight = Random.Range(1, 8),
                handle = Random.Range(1, 8),
                hide = Random.Range(1, 8)
            });
        }

        saveSystem.Add(saveData);
        saveSystem.Save(Files.STATS_FNAME);
        saveSystem.Clear();
        locking = false;
    }
}
