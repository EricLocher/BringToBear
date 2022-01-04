using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadWinScreen : MonoBehaviour
{
    public static bool win = false;
    AsyncOperation winscene;
    private void Start()
    {
        StartCoroutine(LoadScene());
    }
    private void Update()
    {
        if(win)
        {
            winscene.allowSceneActivation = true;
        }
    }
    IEnumerator LoadScene()
    {
        if (Random.Range(0, 100) <= 1)
        {
            winscene = SceneManager.LoadSceneAsync(6);
            winscene.allowSceneActivation = false;
        }
        else
        {
            winscene = SceneManager.LoadSceneAsync(5);
            winscene.allowSceneActivation = false;
        }

        //yield return new WaitUntil(win);
        while (win == false)
        {
            yield return new WaitUntil(() => win == true);
        }
    }
}
