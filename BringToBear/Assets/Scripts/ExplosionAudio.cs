using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionAudio : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip[] explosionSounds;
    float timer;

    void Start()
    {
        timer = 0;
        audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(explosionSounds[Random.Range(0, explosionSounds.Length)]);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer > this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length)
        {
            GetComponent<SpriteRenderer>().enabled = false;
        }
        
        if (!audioSource.isPlaying)
        {
            Destroy(gameObject);
        }
    }
}
