using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public Button startnew;
    public Button continueold;

    public void StartNewPlaythrough()
    {
        SceneManager.LoadScene("Pauzetestscene");
        // ToDo: generate a new playthrough and load the first level of that playthrough, right now its just set to load up my testscene
    }

    public void ContinueOldPlaythrough()
    {
        SceneManager.LoadScene("Pauzetestscene");// just set to load my testscene just like the above method
        // ToDo: make it possible to select an old playthrough? or is there only one playthrough at a time if so then just load up that one
    }
}
