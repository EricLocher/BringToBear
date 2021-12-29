using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreKeeperUI : MonoBehaviour
{
    public ScoreKeeper scoreKeeper;
    public List<GameObject> scoreTexts = new List<GameObject>();
    public GameObject textPrefab;

    private void Update()
    {
        if(scoreTexts.Count < scoreKeeper.ScoreKeeping.Count)
        {
            foreach (PlayerController player in scoreKeeper.ScoreKeeping)
            {
                if (scoreTexts.Count == scoreKeeper.ScoreKeeping.Count) { break; }
                GameObject _text = Instantiate(textPrefab, transform);
                scoreTexts.Add(_text);
            }
        }
        for (int i = 0; i < scoreTexts.Count; i++)
        {
            scoreTexts[i].GetComponent<Text>().text = "Player " + (i + 1).ToString() + " \nDeposited: " + scoreKeeper.ScoreKeeping[i].coinsDeposited.ToString();
            scoreTexts[i].GetComponent<Text>().text += " \nIn Cargo: " + scoreKeeper.ScoreKeeping[i].coinsOnPlayer.ToString();
        }
    }
}
