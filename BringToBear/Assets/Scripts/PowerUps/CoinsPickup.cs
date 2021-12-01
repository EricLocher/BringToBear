using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinsPickup : MonoBehaviour, IInteractable
{
    public int score;
    ScoreKeeper scoreKeeper;

    void Awake()
    {
        score = 1000;
    }

    void Start()
    {
        scoreKeeper = GameObject.FindGameObjectWithTag("GameController").GetComponent<ScoreKeeper>();
    }

    public void Interact(PlayerController player)
    {
        scoreKeeper.AddScore(player, score);
        Debug.Log(score + " Hej!");
        Destroy(gameObject);
    }
}
