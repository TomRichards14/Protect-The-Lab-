using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ButtonManagerScript : MonoBehaviour {

    public Transform MainScreenCanvas;
    public Transform HelpScreenCanvas;
    public Transform GameScenePlayingCanvas;
    public Transform GameScenePausedCanvas;

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

    public void LoadMainMenuCanvas()
    {
        MainScreenCanvas.gameObject.SetActive(true);
        HelpScreenCanvas.gameObject.SetActive(false);
    }

    public void PauseGame()
    {
        GameScenePlayingCanvas.gameObject.SetActive(false);
        GameScenePausedCanvas.gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    public void ContinueGame()
    {
        GameScenePlayingCanvas.gameObject.SetActive(true);
        GameScenePausedCanvas.gameObject.SetActive(false);
        Time.timeScale = 1;
    }
}
