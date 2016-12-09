using Assets.Scripts.Base;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
public class ScenesSaver : MonoBehaviour
{
    private static SaveSystem saveSystem;
    private static int sceneAmount;

    static ScenesSaver()
    {
        saveSystem = new SaveSystem();
        SaveScenes();
    }

    static void Update()
    {
        EditorBuildSettingsScene[] scenes = EditorBuildSettings.scenes;
        if (scenes.Length != sceneAmount)
        {
            SaveScenes();
        }
    }

    private static void SaveScenes()
    {
        Debug.Log("Saving Unity scenes to file");
        List<TinyScene> saveData = new List<TinyScene>();
        EditorBuildSettingsScene[] scenes = EditorBuildSettings.scenes;
        for (int i = 0; i < scenes.Length; ++i)
        {
            if (scenes[i].enabled)
            {
                int ind = i;
                saveData.Add(new TinyScene()
                {
                    name = scenes[i].path,
                    index = ind
                });
            }
            else
            {
                Debug.LogWarning("Scene #" + i + " : " + scenes[i].path + " is not enabled\r\nIf you want to be able to load this scene, enable it");
            }
        }
        sceneAmount = saveData.Count;

        saveSystem.Clear();
        saveSystem.Add(saveData);
        saveSystem.Save("Levels.data");
        saveSystem.Clear();
    }
}
