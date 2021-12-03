using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RailgunPickup : MonoBehaviour, IInteractable
{
    public Gun railGun;
    public void Interact(PlayerController player)
    {
        Debug.Log(player + "Hej");
        player.attack.myGun = railGun;
        Destroy(gameObject);
        //TODO: Give railgun to player who collided with pickup
    }
}
