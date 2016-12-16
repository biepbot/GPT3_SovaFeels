using Assets.Scripts.Base;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitController : MonoBehaviour {

    public GameObject endGamePrefab;

    private void Awake()
    {
        if(endGamePrefab == null)
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
		else
		{
            endGamePrefab.SetActive(true);

            /*
			Debug.LogWarning("Game finished. All " + LevelLoader.DEFAULT_LEVEL_AMOUNT + " levels were finished\r\nUh. Do something now");
            //TODO
            //Code for completing the game.*/
        }
    }
}
