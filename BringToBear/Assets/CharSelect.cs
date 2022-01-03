using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharSelect : MonoBehaviour
{
    public int selectedChar;
    [SerializeField] Image characterSprite;
    public PlayerController playerController;

    public List<Sprite> Characters;

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
        characterSprite.sprite = Characters[selectedChar];
    }

}
