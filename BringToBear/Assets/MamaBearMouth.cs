using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MamaBearMouth : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PlayerCoin"))
        {
            Debug.Log("nom");
            //TODO: Give points to player
        }
    }
}
