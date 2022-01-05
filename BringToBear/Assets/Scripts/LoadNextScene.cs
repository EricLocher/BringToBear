using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadNextScene : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(LoadScene());
    }

    [SerializeField] private Image progressBar;
    IEnumerator LoadScene()
    {
        AsyncOperation gameScene = SceneManager.LoadSceneAsync(4);
        while (gameScene.progress < 1)
        {
            progressBar.fillAmount = gameScene.progress;
            yield return new WaitForEndOfFrame();
        }
    }
}
