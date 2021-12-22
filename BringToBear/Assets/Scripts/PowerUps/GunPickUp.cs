using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPickUp : MonoBehaviour, IInteractable
{

    float spawnForce;
    public Gun gun;

    void Start()
    {
        spawnForce = Random.Range(5, 8);
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.AddForce(Random.insideUnitCircle.normalized * spawnForce, ForceMode2D.Impulse); ;
        Quaternion _rotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
        transform.rotation = _rotation;
    }

    public void Interact(PlayerController player)
    {
        if (player.GetComponent<PlayerAttack>().myGun.infAmmo == true)
        {
            player.GetComponent<PlayerAttack>().SetWeapon(gun);
            Destroy(gameObject);
        }
    }
}
