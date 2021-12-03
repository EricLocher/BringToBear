using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreKeeperUI : MonoBehaviour
{
    public ScoreKeeper scoreKeeper;
    public Text scoreText;

    private void Update()
    {

        string str = "";
        for (int i = 0; i < scoreKeeper.ScoreKeeping.Count; i++)
        {
            str += "Player" + (i + 1).ToString() + " score: " + scoreKeeper.ScoreKeeping[i].score.ToString() + " "; 
        }
        scoreText.text = str;
    }
}
