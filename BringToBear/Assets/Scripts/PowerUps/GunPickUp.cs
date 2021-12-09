using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPickUp : MonoBehaviour, IInteractable
{
    public Gun gun;
    public void Interact(PlayerController player)
    {
        player.SetWeapon(gun);
        Destroy(gameObject);
    }
}
