using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBullet
{
    public void SetOwner(GameObject player);
    public GameObject GetOwner();
    public int GetDamage();
}
