using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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
        //playerController.PlayerInput.SwitchCurrentActionMap("Menu");

    }

    private void Update()
    {
        int readyChk = 0;
        foreach (GameObject portrait in portraits)
        {
            if (portrait.GetComponent<CharSelect>().readyCheck)
            {
                readyChk++;
            }
        }
        if (readyChk >= portraits.Count)
        {
            Ready();
        }
    }
    public void newPlayer(PlayerController player)
    {
        GameObject _temp = Instantiate(portraitPrefub, transform);
        _temp.GetComponent<CharSelect>().playerController = player;
        portraits.Add(_temp);
        pressStart.SetActive(false);
        player.AssignPlayerRoot(_temp);
        player.charPortrait = _temp.GetComponent<CharSelect>();
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
            charSelectScren.SetActive(false);
        }
    }
}
