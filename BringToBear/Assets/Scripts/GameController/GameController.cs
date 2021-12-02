using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameStates gameState;
    public List<PlayerController> Players = new List<PlayerController>();

    void Start()
    {
        for (int i = 0; i < GameObject.FindGameObjectsWithTag("Player").Length; i++)
        {
            Players.Add(GameObject.FindGameObjectsWithTag("Player")[i].GetComponent<PlayerController>());
        }
    }
}