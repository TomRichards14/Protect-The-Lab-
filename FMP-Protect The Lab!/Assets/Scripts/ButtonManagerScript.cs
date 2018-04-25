using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ButtonManagerScript : MonoBehaviour {

    public Transform MainScreenCanvas;
    public Transform HelpScreenCanvas;

    public void ChangeScene(string SceneName)
    {
        SceneManager.LoadScene(SceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadHelpCanvas()
    {
        MainScreenCanvas.gameObject.SetActive(false);
        HelpScreenCanvas.gameObject.SetActive(true);
    }
}
