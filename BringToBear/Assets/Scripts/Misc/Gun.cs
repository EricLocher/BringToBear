using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Gun", menuName = "ScriptableOjects/Gun", order = 0)]
public class Gun : ScriptableObject
{
    public int damage;
    public float fireRate;
    public int ammo;
    public float bulletSpeed;
    public GameObject bullet;
}
