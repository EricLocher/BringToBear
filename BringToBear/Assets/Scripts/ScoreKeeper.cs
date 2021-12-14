using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    public List<PlayerController> ScoreKeeping;
    private void Start()
    {
        ScoreKeeping = GameController.Players;
    }

    public void AddScore(PlayerController player, int score)
    {
        player.coinsOnPlayer += score;
    }
}