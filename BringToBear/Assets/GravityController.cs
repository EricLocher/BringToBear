using UnityEngine;

public class GravityController : MonoBehaviour
{
    public GameObject tunnel;
    [SerializeField, Range(0, 300)]
    float highGravityHeight = 90;
    [SerializeField, Range(0, 300)]
    float lowGravityHeight = 130;
    Bounds tunnelBounds;

    private void Start()
    {
        tunnelBounds = tunnel.GetComponent<SpriteRenderer>().bounds;
    }

    private void Update()
    {
        foreach (PlayerController player in GameController.Players)
        {
            Vector2 _playerPos = player.transform.position;
            Rigidbody2D _rb = player.GetComponent<Rigidbody2D>();

            if (_playerPos.x > (tunnelBounds.center.x + tunnelBounds.extents.x) || _playerPos.x < (tunnelBounds.center.x - tunnelBounds.extents.x))
            {
                if (_playerPos.y < (tunnelBounds.min.y + lowGravityHeight))
                    _rb.gravityScale = 3;
                else
                   _rb.gravityScale = 6;

                Vector3 _dis = (Vector2)tunnelBounds.center - _playerPos;
                _dis = _dis.normalized;
                _rb.AddForce(new Vector3(_dis.x * 25, 0, 0));
            }
            else if (_playerPos.y < (tunnelBounds.min.y + lowGravityHeight))
            {
                _rb.gravityScale = 1;
            }
            else if (_playerPos.y > (tunnelBounds.max.y - highGravityHeight))
            {
                _rb.gravityScale = 6;
            }
            else
            {
                _rb.gravityScale = 3;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Bounds _tunnelBounds = tunnel.GetComponent<SpriteRenderer>().bounds;

        Gizmos.color = Color.green;
        Vector2 lowGravCenter = new Vector2(_tunnelBounds.center.x, _tunnelBounds.min.y);

        Gizmos.DrawWireCube(new Vector2(lowGravCenter.x, lowGravCenter.y + (lowGravityHeight / 2)), new Vector3(1000, lowGravityHeight, 0));

        Gizmos.color = Color.red;
        Vector2 highGravCenter = new Vector2(_tunnelBounds.center.x, _tunnelBounds.max.y);

        Gizmos.DrawWireCube(new Vector2(highGravCenter.x, highGravCenter.y - (highGravityHeight / 2)), new Vector3(_tunnelBounds.extents.x * 2, highGravityHeight, 0));
    }

}
