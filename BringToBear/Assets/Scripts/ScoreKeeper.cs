using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    public List<PlayerController> ScoreKeeping;
    public GameController gameController;
    private void Start()
    {
        ScoreKeeping = gameController.Players;
    }

    public void AddScore(PlayerController player, int score)
    {
        player.score += score;
    }
}