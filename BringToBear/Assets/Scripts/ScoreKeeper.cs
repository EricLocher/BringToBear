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
    AudioSource audioSource;

    private void Start()
    {
        ScoreKeeping = GameController.Players;
        audioSource = GetComponent<AudioSource>();
    }

    public void AddScore(PlayerController player, int score)
    {
        player.coinsOnPlayer += score;
        audioSource.pitch = Random.Range(0.9f, 1.1f);
        audioSource.PlayOneShot(coinPickup[(Random.Range(0, coinPickup.Length))], 0.4f);
    }

    public void DepositScore(PlayerController player, int score)
    {
        player.coinsDeposited += score;
        audioSource.pitch = Random.Range(0.95f, 1.05f);
        audioSource.PlayOneShot(coinDeposit[(Random.Range(0, coinPickup.Length))], 0.8f);
        if (player.coinsDeposited >= WinCondition)
        {
            SceneManager.LoadScene(6);
        }
    }

}