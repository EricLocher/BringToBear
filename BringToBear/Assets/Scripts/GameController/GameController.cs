using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameStates gameState;
    public static List<PlayerController> Players = new List<PlayerController>();
    public static List<GameObject> Indicators = new List<GameObject>();
    static List<GameObject> pSprite;
    public List<GameObject> playerSprite;
    public List<GameObject> offScreenIndicators;
    public static List<GameObject> playerIndicators = new List<GameObject>();
    public ScoreKeeperUI scoreKeeperUI;
    static ScoreKeeperUI scoreController;
    void Start()
    {
        for (int i = 0; i < GameObject.FindGameObjectsWithTag("Player").Length; i++)
        {
            Players.Add(GameObject.FindGameObjectsWithTag("Player")[i].GetComponent<PlayerController>());
            Indicators.Add(GameObject.FindGameObjectsWithTag("Indicator")[i]);
        }

        pSprite = playerSprite;
        Indicators = offScreenIndicators;

        scoreController = scoreKeeperUI;
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
        
    }

    public static void UpdatePlayer(PlayerController player, int selectedPlayer)
    {
        Destroy(player.playerSprite.gameObject);
        GameObject _sprite = Instantiate(pSprite[selectedPlayer], player.transform);
        player.playerSprite = _sprite;
        player.anim = _sprite.GetComponent<ShipAnimation>();
        scoreController.NewPlayer(player, selectedPlayer);
        GameObject _temp = Instantiate(Indicators[selectedPlayer]);
        _temp.GetComponent<OffScreenIndicator>().Player = player.gameObject;
        playerIndicators.Add(_temp);
    }
}