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
        SceneManager.LoadScene(5);
    }
    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
    public void Credits()
    {
        SceneManager.LoadScene(3);
    }
    public void OptionsMenu()
    {
        SceneManager.LoadScene(1);
    }
    public void Gallery()
    {
        SceneManager.LoadScene(2);
    }
    
    public void MainMenu()
    {     
    SceneManager.LoadScene(0);
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
