using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Winner : MonoBehaviour
{
    [SerializeField]List<Sprite> playerSprites;
    [SerializeField] Image Image;

    private void Start()
    {
        switch (ScoreKeeper.WinPlayer)
        {
            case Players.player0:
                Image.sprite = playerSprites[0];
                break;
            case Players.player1:
                Image.sprite = playerSprites[1];
                break;
            case Players.player2:
                Image.sprite = playerSprites[2];
                break;
            case Players.player3:
                Image.sprite = playerSprites[3];
                break;
            default:
                Image.sprite = playerSprites[0];
                break;
        }
    }
}
