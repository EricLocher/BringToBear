using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reset : MonoBehaviour
{
    void Start()
    {
        LoadWinScreen.win = false;
        GameController.playerIndicators.Clear();
        GameController.Players.Clear();
    }

}
