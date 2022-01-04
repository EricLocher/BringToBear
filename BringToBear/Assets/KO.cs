using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KO : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip koSound;
    public AudioClip loseMoneySound;
    public float cameraShake;
    float timer;

    void Start()
    {
        timer = 0;
        audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(koSound, 1.5f);
        audioSource.PlayOneShot(loseMoneySound, 0.8f);
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>().Shake(cameraShake);
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