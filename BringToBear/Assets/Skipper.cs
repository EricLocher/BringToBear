using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using UnityEngine.UI;

public class Skipper : MonoBehaviour
{
    float timer = 0;

    public VideoPlayer videoPlayer;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        timer = Time.deltaTime;

        if (timer > 1 && !videoPlayer.isPlaying || timer > 30)
        {
            SceneManager.LoadScene(3);
        }

        if(Input.anyKey)
        {
            Skip();
        }

    }

    //public void Skip(InputAction.CallbackContext value)
    public void Skip()
    {
            SceneManager.LoadScene(3);
        //videoPlayer.Stop();
    }

}
