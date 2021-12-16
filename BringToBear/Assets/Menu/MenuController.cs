using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Jonas");
    }
    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
    public void Placeholder()
    {
        //SceneManager.LoadScene("");
    }
    public void OptionsMenu()
    {
        //SceneManager.LoadScene("Options");
    }
    public void Gallery()
    {
        //SceneManager.LoadScene("Gallery");
    }
}
