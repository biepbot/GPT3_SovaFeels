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
        SaveStats();
    }

    // Temporary fields and method to create stats file with dummy data
    private static SaveSystem saveSystem = new SaveSystem();
    private static bool locking = false;

    private void SaveStats()
    {
        if (locking) return;

        locking = true;
        saveSystem.Clear();
        Debug.Log("Saving dummy data to stats file");
        //List<CategorySettings> saveData = new List<CategorySettings>();
        List<CategorySettings> saveData = new List<CategorySettings>();

        saveData.Add(new CategorySettings()
        {
            categoryName = "je moeder einz", //nee zeggen
            fight = 4,
            handle = 1,
            hide = 3
        });

        saveData.Add(new CategorySettings()
        {
            categoryName = "je moeder zwei", //in je recht staan
            fight = 1,
            handle = 5,
            hide = 2
        });

        saveData.Add(new CategorySettings()
        {
            categoryName = "je moeder einz", //nee zeggen
            fight = 4,
            handle = 1,
            hide = 3
        });

        saveData.Add(new CategorySettings()
        {
            categoryName = "je moeder zwei", //in je recht staan
            fight = 1,
            handle = 5,
            hide = 2
        });

        saveData.Add(new CategorySettings()
        {
            categoryName = "je moeder einz", //nee zeggen
            fight = 4,
            handle = 1,
            hide = 3
        });

        saveData.Add(new CategorySettings()
        {
            categoryName = "je moeder zwei", //in je recht staan
            fight = 1,
            handle = 5,
            hide = 2
        });

        saveData.Add(new CategorySettings()
        {
            categoryName = "je moeder einz", //nee zeggen
            fight = 4,
            handle = 1,
            hide = 3
        });

        saveData.Add(new CategorySettings()
        {
            categoryName = "je moeder zwei", //in je recht staan
            fight = 1,
            handle = 5,
            hide = 2
        });

        saveData.Add(new CategorySettings()
        {
            categoryName = "je moeder einz", //nee zeggen
            fight = 4,
            handle = 1,
            hide = 3
        });

        saveData.Add(new CategorySettings()
        {
            categoryName = "je moeder zwei", //in je recht staan
            fight = 1,
            handle = 5,
            hide = 2
        });

        saveData.Add(new CategorySettings()
        {
            categoryName = "je moeder einz", //nee zeggen
            fight = 4,
            handle = 1,
            hide = 3
        });

        saveData.Add(new CategorySettings()
        {
            categoryName = "je moeder zwei", //in je recht staan
            fight = 1,
            handle = 5,
            hide = 2
        });


        //saveSystem.AddObjectsIndivually(saveData);
        saveSystem.Add(saveData);
        saveSystem.Save(Files.STATS_FNAME);
        saveSystem.Clear();
        locking = false;
    }
}
