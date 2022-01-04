using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreKeeperUI : MonoBehaviour
{
    List<GameObject> portraits = new List<GameObject>();
    [SerializeField] List<Sprite> sprites = new List<Sprite>();
    [SerializeField] GameObject prefabs;

    public void NewPlayer(PlayerController player, int selectedplayer)
    {
        GameObject _temp = Instantiate(prefabs, transform);
        _temp.GetComponent<PlayerPortrait>().Create(player, sprites[selectedplayer]);
    }

}
