using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinsPickup : MonoBehaviour, IInteractable
{
    public int score;
    public void Interact(PlayerController player)
    {
        score = score + 1000;
        Debug.Log(score + " Hej!");
        Destroy(gameObject);
    }
}
