using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPickUp : MonoBehaviour, IInteractable
{


    public Gun gun;

    void Start()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.AddForce(Random.insideUnitCircle.normalized * 5, ForceMode2D.Impulse);
        Quaternion _rotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
        transform.rotation = _rotation;
    }

    public void Interact(PlayerController player)
    {
        player.GetComponent<PlayerAttack>().SetWeapon(gun);
        Destroy(gameObject);
    }
}
