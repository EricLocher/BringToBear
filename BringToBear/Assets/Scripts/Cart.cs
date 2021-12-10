using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Cart : MonoBehaviour, ICharacter
{
    public Cart connectedCart;
    public int cartHP = 100;

    public List<GameObject> Pickups;
    private GameObject anchor;

    private void Start()
    {
        anchor = transform.GetChild(0).gameObject;
    }

    private void Update()
    {
        if (cartHP <= 0)
        {
            transform.parent.GetComponent<Train>().DestroyedCart(gameObject);
            int selectedPickup = Random.Range(0, Pickups.Count - 1);

            for (int i = 0; i < 25; i++)
            {
                Instantiate(Pickups[0], transform.position, Quaternion.identity);
            }

            Destroy(gameObject);
        }

        LookAt();
        Move();
    }

    public void Move()
    {
        if (connectedCart == null) { return; }

        Vector2 _cartPos = connectedCart.transform.position;

        if (transform.position.x != _cartPos.x)
        {
            transform.position = new Vector2(Mathf.Lerp(transform.position.x, _cartPos.x, 0.05f), transform.position.y);
        }
    }

    public void LookAt()
    {
        if (connectedCart == null) { return; }

        try
        {
            Vector2 _cartAnchor = connectedCart.anchor.transform.position;
            Vector2 diff = _cartAnchor - (Vector2)transform.position;
            diff.Normalize();

            float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
        }
        catch (System.Exception e)
        {
            return;
        }
    }

    public void Damage(int amount)
    {
        cartHP = cartHP - amount;
        int dropTable = Random.Range(0, 21);

        if (dropTable < 10)
        {
            Instantiate(Pickups[0], transform.position, Quaternion.identity);
        }
        if (dropTable == 11)
        { 
            Instantiate(Pickups[1], transform.position, Quaternion.identity);
        }

    }
}
