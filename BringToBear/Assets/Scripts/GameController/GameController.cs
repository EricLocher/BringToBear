using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameStates gameState;

    public void ChangeSate(GameStates state)
    {
        gameState = state;
        UpdateState();
    }

    private void UpdateState()
    {
        switch (gameState)
        {
            case GameStates.Paused:
                //TODO: Add pause function.
                break;
            case GameStates.Playing:
                //TODO: Add play function.
                break;
            case GameStates.GameOver:
                //TODO: Add gameover function.
                break;
            default:
                break;
        }

    }




}
