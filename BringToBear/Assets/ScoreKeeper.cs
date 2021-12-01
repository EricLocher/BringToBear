using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    public int p1Score;
    public int p2Score;

    public List<GameObject> ScoreKeeping = new List<GameObject>();

    private void Start()
    {
        for (int i = 0; i < GameObject.FindGameObjectsWithTag("Player").Length; i++)
        {
            ScoreKeeping.Add(GameObject.FindGameObjectsWithTag("Player")[i]);

        }
    }

    public void Scorekeeping()
    {
        foreach (GameObject player in ScoreKeeping)
        {

        }
    }
   
}
