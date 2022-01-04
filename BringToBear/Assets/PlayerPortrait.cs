using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPortrait : MonoBehaviour
{
    [SerializeField] PlayerController player;
    [SerializeField] Text HP;
    [SerializeField] Text score;
    [SerializeField] Text scoreOnPlayer;
    [SerializeField] Image portrait;
    public void Create(PlayerController player, Sprite sprite)
    {
        this.player = player;
        portrait.sprite = sprite;

    }

    void Update()
    {
        HP.text = (int)(player.damageTaken / 20) + " ö";
        score.text = player.coinsDeposited + "";
        scoreOnPlayer.text = player.coinsOnPlayer + "";
    }
}
