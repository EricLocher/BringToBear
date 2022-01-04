using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadWinScreen : MonoBehaviour
{
    bool win = false;

    private void Start()
    {
        StartCoroutine(LoadScene());
    }
    private void Update()
    {
        if (GameController.gameState == GameStates.GameOver)
        {
            win = true;
        }
    }
    IEnumerator LoadScene()
    {
        if (Random.Range(0, 100) <= 1)
        {
            AsyncOperation win1 = SceneManager.LoadSceneAsync(6);
        }
        else
        {
            AsyncOperation win2 = SceneManager.LoadSceneAsync(5);
        }

        while (win == false)
        {
            yield return new WaitUntil(() => win);

        }
    }
}
