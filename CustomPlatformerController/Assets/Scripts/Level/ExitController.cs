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
        if (LevelLoader.HasMoreLevels)
        {
            LevelLoader.LoadNextLevel();
        }
        else if (SceneManager.GetActiveScene().name == "Tutorial")
        {
            SceneManager.LoadScene("MainMenuScene");
        }
        else if (!playthroughEnded)
        {
            RewardsText rewardsText = endGamePrefab.GetComponentInChildren<RewardsText>();
            if(rewardsText != null)
            {
                rewardsText.SetText();
            }
            LevelLoader.EndPlaythrough();
            playthroughEnded = true;
            endGamePrefab.SetActive(true);
        }
    }
}
