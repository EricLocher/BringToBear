using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreKeeper : MonoBehaviour
{
    public List<PlayerController> ScoreKeeping;
    public int WinCondition = 200;
    public AudioClip[] coinPickup;
    public AudioClip[] coinDeposit;
    public AudioSource audioSource0;
    public AudioSource audioSource1;

    public static Players WinPlayer;

    private void Start()
    {
        ScoreKeeping = GameController.Players;
        audioSource0 = GetComponent<AudioSource>();
    }

    public void AddScore(PlayerController player, int score)
    {
        player.coinsOnPlayer += score;
        if (!audioSource0.isPlaying)
        {
            audioSource0.pitch = Random.Range(0.9f, 1.1f);
            audioSource0.PlayOneShot(coinPickup[(Random.Range(0, coinPickup.Length))], 0.4f);
        }
        else if (audioSource0.isPlaying && !audioSource1.isPlaying)
        {
            audioSource1.pitch = Random.Range(0.9f, 1.1f);
            audioSource1.PlayOneShot(coinPickup[(Random.Range(0, coinPickup.Length))], 0.25f);
        }
        else
            audioSource0.Stop();
    }

    public void DepositScore(PlayerController player, int score)
    {
       
        player.coinsDeposited += score;

        if (player.coinsDeposited >= WinCondition)
        {
            LoadWinScreen.win = true;
            //if(randomNum <= 1)
            //{
            //    SceneManager.LoadScene(6);
            //    return;
            //}

            WinPlayer = player.player;
            //SceneManager.LoadScene(5);
            //return;
        }

        if (!audioSource0.isPlaying)
        {
            audioSource0.pitch = Random.Range(0.95f, 1.05f);
            audioSource0.PlayOneShot(coinDeposit[(Random.Range(0, coinPickup.Length-1))], 0.8f);
        }
        else if (audioSource0.isPlaying && !audioSource1.isPlaying)
        {
            audioSource1.pitch = Random.Range(0.9f, 1.1f);
            audioSource1.PlayOneShot(coinPickup[(Random.Range(0, coinPickup.Length-1))], 0.4f);
        }
        else
            audioSource0.Stop();
    }

}