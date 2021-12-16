using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{

    [SerializeField] PlayerInputManager PlayerManager;
    public List<PlayerInput> playerList = new List<PlayerInput>();
    public GameController gameController;
    public GameObject indicator;

    [SerializeField] InputAction joinAction, leaveAction;

    public event System.Action<PlayerInput> PlayerJoinedGame;
    public event System.Action<PlayerInput> PlayerLeftGame;

    private void Awake()
    {
        joinAction.Enable();
        joinAction.performed += context => JoinAction(context);
    }

    void JoinAction(InputAction.CallbackContext context)
    {
        PlayerInputManager.instance.JoinPlayerFromActionIfNotAlreadyJoined(context);
    }

    void OnPlayerJoined(PlayerInput playerInput)
    {
        Debug.Log("Player Joined The Game!");
        playerList.Add(playerInput);
        GameController.Players.Add(playerInput.GetComponent<PlayerController>());
        
        GameObject _temp = Instantiate(indicator);
        _temp.GetComponent<OffScreenIndicator>().Player = playerInput.gameObject;
        GameController.Indicators.Add(_temp.GetComponent<OffScreenIndicator>());
        
        if(PlayerJoinedGame != null)
        {
            PlayerJoinedGame(playerInput);
        }

    }

    void OnPlayerLeft(PlayerInput playerInput)
    {
        Debug.LogWarning("FUNCTION NOT IMPLEMENTED -- Don't mind this for now...");
    }
}
