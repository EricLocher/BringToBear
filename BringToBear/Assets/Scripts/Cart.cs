using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cart : MonoBehaviour
{
    public Cart connectedCart;
    public Vector2 anchor;
    public int cartHP = 100;

    public bool active = true;

    public List<GameObject> Pickups;

    private void Update()
    {
        if(cartHP <= 0)
        {
            transform.parent.GetComponent<Train>().DestroyedCart(gameObject);
            int selectedPickup = Random.Range(0, Pickups.Count - 1);
            //Instantiate(Pickups[selectedPickup]);
            Destroy(this.gameObject);
        }

        if(active)
        Move();
    }

    public void Move()
    {
        Vector2 _cartTransform = connectedCart.transform.position;
        Vector2 _cartAnchor = connectedCart.anchor;
        Vector2 diff = (_cartTransform + _cartAnchor) - (Vector2)transform.position;
        diff.Normalize();

        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
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

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere((Vector2)transform.position + anchor, 0.5f);
    }
}
