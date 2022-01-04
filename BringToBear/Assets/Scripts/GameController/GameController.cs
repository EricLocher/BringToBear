using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameStates gameState;
    public static List<PlayerController> Players = new List<PlayerController>();
    public static List<OffScreenIndicator> Indicators = new List<OffScreenIndicator>();
    static List<GameObject> pSprite;
    static GameObject ind;
    public List<GameObject> playerSprite;
    public GameObject indicator;

    void Start()
    {
        for (int i = 0; i < GameObject.FindGameObjectsWithTag("Player").Length; i++)
        {
            Players.Add(GameObject.FindGameObjectsWithTag("Player")[i].GetComponent<PlayerController>());
            Indicators.Add(GameObject.FindGameObjectsWithTag("Indicator")[i].GetComponent<OffScreenIndicator>());
        }

        pSprite = playerSprite;
        ind = indicator;
    }

    public static GameStates GetGamestate()
    {
        return gameState;
    }

    public static void ChangeGameState(GameStates state)
    {
        gameState = state;
    }

    public static void NewPlayer(PlayerController player)
    {
        Players.Add(player.GetComponent<PlayerController>());
        GameObject _sprite = Instantiate(pSprite[0], player.transform);
        player.playerSprite = _sprite;
        player.anim = _sprite.GetComponent<ShipAnimation>();
        GameObject _temp = Instantiate(ind);
        _temp.GetComponent<OffScreenIndicator>().Player = player.gameObject;
        Indicators.Add(_temp.GetComponent<OffScreenIndicator>());
    }

    public static void UpdatePlayer(PlayerController player, int selectedPlayer)
    {
        Destroy(player.playerSprite.gameObject);
        GameObject _sprite = Instantiate(pSprite[selectedPlayer], player.transform);
        player.playerSprite = _sprite;
        player.anim = _sprite.GetComponent<ShipAnimation>();
    }
}