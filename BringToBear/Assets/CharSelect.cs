using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class CharSelect : MonoBehaviour
{
    public int selectedChar;
    [SerializeField] Image characterSprite;
    public PlayerController playerController;
    [SerializeField] Button readyButton;
    [SerializeField] Sprite sprittjevel;

    public List<Sprite> Characters;
    public bool readyCheck = false;

    void Start()
    {
        selectedChar = transform.parent.GetComponent<CharSelectController>().portraits.Count - 1;
        characterSprite.sprite = Characters[selectedChar];
    }

    public void LeftButton()
    {
        selectedChar--;
        if (selectedChar < 0)
        {
            selectedChar = Characters.Count - 1;
        }
        characterSprite.sprite = Characters[selectedChar];
    }

    public void RightButton()
    {
        selectedChar++;
        
        if (selectedChar > Characters.Count - 1)
        {
            selectedChar = 0;
        }
        foreach (GameObject other in transform.parent.GetComponent<CharSelectController>().portraits)
        {
            if (other == gameObject)
            {
                continue;
            }
            if (other.GetComponent<CharSelect>().selectedChar == selectedChar)
            {
                selectedChar++;
                if (selectedChar > Characters.Count - 1)
                {
                    RightButton();
                    return;
                }
            }
        }
        characterSprite.sprite = Characters[selectedChar];
    }

    public void ReadyButton()
    {
        Debug.Log("Blop");
        playerController.GetComponent<PlayerInput>().SwitchCurrentActionMap("Controls");
        readyCheck = true;
        transform.GetChild(4).GetComponent<Image>().sprite = sprittjevel;
        transform.GetChild(4).GetComponent<Button>().enabled = false;
    }
}
