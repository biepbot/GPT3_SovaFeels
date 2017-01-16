﻿using System;
using Assets.Scripts.Base;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitController : MonoBehaviour
{
    public GameObject endGamePrefab;
    public GameStats gameStats { get { return GameStats.Instance; } }

    public bool playthroughEnded = false;

    public static bool endLevelSoundPlayed;

    private void Awake()
    {
        if (endGamePrefab == null)
        {
            endGamePrefab = Instantiate(Resources.Load("EndMenu/EndGameMenu", typeof(GameObject))) as GameObject;

            endGamePrefab.SetActive(false);
        }
    }

    public void NextLevel()
    {
        if (!endLevelSoundPlayed)
        {
            if (SoundManager.Instance != null)
            {
                SoundManager.Instance.ObjectSounds[0].PlayAudioClip(5);
            }
            endLevelSoundPlayed = true;
        }

        if (LevelLoader.HasMoreLevels)
        {
            NPCController.SolutionTypes solution = MontyController.GetSolution();
            switch (solution)
            {
                case NPCController.SolutionTypes.Confrontation:
                    gameStats.IncreaseConfrontation(gameStats.levelDifficulty.ToString());
                    break;
                case NPCController.SolutionTypes.Ignore:
                case NPCController.SolutionTypes.Flight:
                    gameStats.IncreaseIgnoreFlight(gameStats.levelDifficulty.ToString());
                    break;
                case NPCController.SolutionTypes.Fight:
                    gameStats.IncreaseFight(gameStats.levelDifficulty.ToString());
                    break;
            }

            gameStats.Save();

            LevelLoader.LoadNextLevel();
        }
        else if (SceneManager.GetActiveScene().name == "Tutorial")
        {
            SceneManager.LoadScene("MainMenuScene");
            endLevelSoundPlayed = false;
        }
        else if (!playthroughEnded)
        {
            NPCController.SolutionTypes solution = MontyController.GetSolution();
            switch (solution)
            {
                case NPCController.SolutionTypes.Confrontation:
                    gameStats.IncreaseConfrontation(gameStats.levelDifficulty.ToString());
                    break;
                case NPCController.SolutionTypes.Ignore:
                case NPCController.SolutionTypes.Flight:
                    gameStats.IncreaseIgnoreFlight(gameStats.levelDifficulty.ToString());
                    break;
                case NPCController.SolutionTypes.Fight:
                    gameStats.IncreaseFight(gameStats.levelDifficulty.ToString());
                    break;
            }

            RewardsText rewardsText = endGamePrefab.GetComponentInChildren<RewardsText>();
            if (rewardsText != null)
            {
                rewardsText.SetText();
            }

            LevelLoader.EndPlaythrough();
            playthroughEnded = true;
            endGamePrefab.SetActive(true);
            endLevelSoundPlayed = false;
            gameStats.EndPlaythrough(DateTime.Now);
            gameStats.Save();
        }
    }
}
