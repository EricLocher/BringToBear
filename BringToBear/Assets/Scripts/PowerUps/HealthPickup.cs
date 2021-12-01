using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour, IInteractable
{
    public void Interact(PlayerController player)
    {
        Debug.Log(player);
        //TODO: Give health to player who collided with pickup
    }  
}
