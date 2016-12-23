using Assets.Scripts.Base;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitController : MonoBehaviour
{

    public GameObject endGamePrefab;
    public GameStats gameStats;

    public bool playthroughEnded = false;

    private void Awake()
    {
        gameStats = GameObject.FindObjectOfType<GameStats>();

        if (gameStats == null)
        {
            gameStats = (Instantiate(Resources.Load("Stats/GameStats", typeof(GameObject))) as GameObject).GetComponent<GameStats>();
        }

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
            gameStats.RewardCoins(5);
            gameStats.Save();
            playthroughEnded = true;
            endGamePrefab.SetActive(true);

            /*
			Debug.LogWarning("Game finished. All " + LevelLoader.DEFAULT_LEVEL_AMOUNT + " levels were finished\r\nUh. Do something now");
            //TODO
            //Code for completing the game.*/
        }
    }
}
