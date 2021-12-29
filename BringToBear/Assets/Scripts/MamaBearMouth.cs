using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MamaBearMouth : MonoBehaviour
{
    public PointEffector2D suction;
    public static GameStates state;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PlayerCoin"))
        {
            Debug.Log("nom");
            //TODO: Give points to player
        }
    }

    private void Update()
    {

        if (GameController.GetGamestate() == GameStates.Collecting)
        {
            suction.enabled = true;
        }
        else
            suction.enabled = false;

    }

}
