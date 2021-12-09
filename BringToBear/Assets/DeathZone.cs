using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    [SerializeField] float height;
    [SerializeField] float width;

    private void Update()
    {
        foreach (PlayerController player in GameController.Players)
        {
            if (
                Mathf.Abs((player.transform.position.x - transform.position.x)) > width/2
                ||
                Mathf.Abs((player.transform.position.y - transform.position.y)) > height/2
              )
            {
                player.res();
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireCube(transform.position, new Vector3(width, height, 0));

    }
}
