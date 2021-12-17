using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameStates gameState;
    public static List<PlayerController> Players = new List<PlayerController>();
    public static List<OffScreenIndicator> Indicators = new List<OffScreenIndicator>();
    
    void Start()
    {
        for (int i = 0; i < GameObject.FindGameObjectsWithTag("Player").Length; i++)
        {
            Players.Add(GameObject.FindGameObjectsWithTag("Player")[i].GetComponent<PlayerController>());
            Indicators.Add(GameObject.FindGameObjectsWithTag("Indicator")[i].GetComponent<OffScreenIndicator>());
        }
    }





}