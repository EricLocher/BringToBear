using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour, IBullet
{
    public float speed;
    public int damage;
    GameObject Owner;
    List<GameObject> Players = new List<GameObject>();

    public void SetOwner(GameObject player)
    {
        Owner = player;
    }

    void Start()
    {
        transform.GetComponent<Rigidbody2D>().velocity = transform.up * speed;
    }



}
