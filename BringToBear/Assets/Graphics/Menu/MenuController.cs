using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public GameObject NormalCanvas;
    public GameObject KeybindCanvas;
    UnityEngine.UI.Button resumeButton;

    public void StartGame()
    {
        SceneManager.LoadScene(7);
    }
    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
    public void Credits()
    {
        SceneManager.LoadScene("Credits");
    }
    public void OptionsMenu()
    {
        SceneManager.LoadScene("SettingsMenu");
    }   
    public void MainMenu()
    {     
        SceneManager.LoadScene("MainMenu");
    }
    public void Keybinds()
    {
        NormalCanvas.SetActive(false);
        KeybindCanvas.SetActive(true);
    }
    public void BackFromKeybinds()
    {
        KeybindCanvas.SetActive(false);
        NormalCanvas.SetActive(true);
    }
}
