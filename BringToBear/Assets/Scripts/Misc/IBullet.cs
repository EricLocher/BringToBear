using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBullet
{
    GameObject Owner { get; set; }
    GameObject OwnerShield { get; set; }
}
