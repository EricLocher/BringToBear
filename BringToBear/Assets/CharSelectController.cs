using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharSelectController : MonoBehaviour
{
    public List<GameObject> portraits = new List<GameObject>();
    [SerializeField] GameObject portraitPrefub;
    [SerializeField] GameObject charSelectScren;
    [SerializeField] GameObject pressStart;
    [SerializeField] GameObject readyCheck;

    [SerializeField] private PlayerController playerController = null;

    void Start()
    {
        Time.timeScale = 0;
        playerController.PlayerInput.SwitchCurrentActionMap("Menu");

    }
    public void newPlayer(PlayerController player)
    {
        GameObject _temp = Instantiate(portraitPrefub, transform);
        _temp.GetComponent<CharSelect>().playerController = player;
        portraits.Add(_temp);
        pressStart.SetActive(false);
        readyCheck.SetActive(true);
    }

    public void Ready()
    {
        if (readyCheck.activeInHierarchy)
        {
            foreach (GameObject player in portraits)
            {
                GameController.UpdatePlayer(player.GetComponent<CharSelect>().playerController, player.GetComponent<CharSelect>().selectedChar);
            }
            Time.timeScale = 1;
            playerController.PlayerInput.SwitchCurrentActionMap("Controls");
            charSelectScren.SetActive(false);
        }
    }
}
