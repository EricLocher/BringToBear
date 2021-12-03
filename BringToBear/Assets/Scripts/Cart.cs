using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cart : MonoBehaviour
{
    public Rigidbody2D connectedCart;
    public int cartHP = 100;

    public List<GameObject> Pickups;

    private void Update()
    {
        if(cartHP <= 0)
        {
            transform.parent.GetComponent<Train>().DestroyedCart(gameObject);
            int selectedPickup = Random.Range(0, Pickups.Count - 1);
            Instantiate(Pickups[selectedPickup]);
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Projectile"))
        {
            IBullet _bullet = other.GetComponent<IBullet>();
            if (_bullet == null) { Debug.LogError("THE OBJECT DOES NOT HAVE THE BULLET INTERFACE"); return; }
            cartHP -= _bullet.GetDamage();
            Destroy(other.gameObject);
            Debug.Log(cartHP);
        }
    }
}
