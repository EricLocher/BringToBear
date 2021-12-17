using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip[] songList;


    void Start()
    {
        int random = Random.Range(0, 30);
        audioSource = GetComponent<AudioSource>();
        RandomizeSong();
    }

    void Update()
    {
        if (!audioSource.isPlaying)
        {
            RandomizeSong();
        }
    }

    public void RandomizeSong()
    {
        audioSource.Stop();
        int random = Random.Range(0, 30);
        audioSource.PlayOneShot(songList[random]);
    }
}