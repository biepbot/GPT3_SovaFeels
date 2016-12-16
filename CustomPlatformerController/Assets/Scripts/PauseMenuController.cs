using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
    public Button pausebutton;
    public Button resumebutton;
    public Button mainmenubutton;
    public GameObject pausepanel;

    public void PauseGame()
    {
        pausepanel.SetActive(true);
        pausebutton.gameObject.SetActive(false);

        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        pausepanel.SetActive(false);
        pausebutton.gameObject.SetActive(true);

        Time.timeScale = 1;
    }

    public void ReturnToMain()
    {
        // ToDo: save all character stuff that hasnt been saved
        Time.timeScale = 1;

        SceneManager.LoadScene("MainMenuScene");
    }
}
