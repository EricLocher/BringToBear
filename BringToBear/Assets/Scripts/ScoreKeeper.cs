using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreKeeper : MonoBehaviour
{
    public List<PlayerController> ScoreKeeping;
    public int WinCondition = 200;
    private void Start()
    {
        ScoreKeeping = GameController.Players;
    }

    public void AddScore(PlayerController player, int score)
    {
        player.coinsOnPlayer += score;
        
    }

    public void DepositScore(PlayerController player, int score)
    {
        player.coinsDeposited += score;
        if (player.coinsDeposited >= WinCondition)
        {
            SceneManager.LoadScene(6);
        }
    }

}