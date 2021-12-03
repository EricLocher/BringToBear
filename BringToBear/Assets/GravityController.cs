using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityController : MonoBehaviour
{
    public GameObject tunnel;
    public float highGravityHeight;
    public float lowGravityHeight;
    public GameController gameController;
    Bounds tunnelBounds;

    private void Start()
    {
        tunnelBounds = tunnel.GetComponent<SpriteRenderer>().bounds;
    }

    private void Update()
    {
        foreach (PlayerController player in gameController.Players)
        {
            Vector2 playerPos = player.transform.position;
            if (playerPos.x > (tunnelBounds.center.x + tunnelBounds.extents.x) || playerPos.x < (tunnelBounds.center.x - tunnelBounds.extents.x))
            {
                player.GetComponent<Rigidbody2D>().gravityScale = 6;
            }
            else if (playerPos.y > (tunnelBounds.max.y - highGravityHeight))
            {
                player.GetComponent<Rigidbody2D>().gravityScale = 6;
            }
            else if (playerPos.y < (tunnelBounds.min.y + lowGravityHeight))
            {
                player.GetComponent<Rigidbody2D>().gravityScale = 1;
            }
            else
            {
                player.GetComponent<Rigidbody2D>().gravityScale = 3;
            } 
        }
    }

    private void OnDrawGizmos()
    {
        Bounds _tunnelBounds = tunnel.GetComponent<SpriteRenderer>().bounds;

        Gizmos.color = Color.green;
        Vector2 lowGravCenter = new Vector2(_tunnelBounds.center.x, _tunnelBounds.min.y);

        Gizmos.DrawWireCube(new Vector2(lowGravCenter.x, lowGravCenter.y + (lowGravityHeight/2)), new Vector3(_tunnelBounds.extents.x*2, lowGravityHeight, 0));
        
        Gizmos.color = Color.red;
        Vector2 highGravCenter = new Vector2(_tunnelBounds.center.x, _tunnelBounds.max.y);

        Gizmos.DrawWireCube(new Vector2(highGravCenter.x, highGravCenter.y - (highGravityHeight / 2)), new Vector3(_tunnelBounds.extents.x * 2, highGravityHeight, 0));
    }

}
