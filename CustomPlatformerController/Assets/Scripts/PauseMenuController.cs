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

        // ToDo: disable player controls and maybe also pause the game(stop all moving things and stuff) instead of just showing a menu overlay
    }

    public void ResumeGame()
    {
        pausepanel.SetActive(false);
        pausebutton.gameObject.SetActive(true);
    }

    public void ReturnToMain()
    {
        // ToDo: save all character stuff that hasnt been saved
        SceneManager.LoadScene("MainMenuScene");
    }
}
