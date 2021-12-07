using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour, IBullet
{
    public float maxSpeed, acceleration;
    public float trackRadius;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] GameObject Explosion;

    public int damage = 10;

    float currentTrackRadius = 0;
    GameObject Owner;
    GameObject currentTarget;
    List<GameObject> Players = new List<GameObject>();

    void Start()
    {
        currentTrackRadius = trackRadius;
        GameObject[] _players = GameObject.FindGameObjectsWithTag("Player");

        for (int i = 0; i < _players.Length; i++)
        {
            Players.Add(_players[i]);
        }
    }

    void Update()
    {
        foreach (GameObject _player in Players)
        {
            if (_player == Owner) { continue; }

            Vector3 _playerPos = _player.transform.position;

            //Check if player lies within the tracking radius
            if (Mathf.Pow(_playerPos.x - transform.position.x, 2) + Mathf.Pow(_playerPos.y - transform.position.y, 2) < Mathf.Pow(currentTrackRadius, 2))
            {
                float dist = Vector2.SqrMagnitude(_player.transform.position - transform.position);
                if(currentTarget == null)
                {
                    currentTarget = _player;
                }
                else if(dist < Vector2.SqrMagnitude(currentTarget.transform.position - transform.position)) {
                    currentTarget = _player;
                }
            }
        }
        Track();
    }
    void Track()
    {
        if (currentTarget != null)
        {
            Vector3 direction = currentTarget.transform.position - transform.position;
            float dist = Vector2.SqrMagnitude(currentTarget.transform.position - transform.position);
            direction.Normalize();

            float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, rotZ - 90), Time.deltaTime * 20f);
            rb.AddForce(transform.up * Time.deltaTime * (acceleration), ForceMode2D.Impulse);
        }
        else
        {
            rb.AddForce(transform.up * Time.deltaTime * acceleration, ForceMode2D.Impulse);
        }
        rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxSpeed);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {

        if(!other.CompareTag("Projectile"))
        if (other.gameObject != Owner)
        {
            if (other.GetComponent<ICharacter>() != null)
            {
                other.GetComponent<ICharacter>().Damage(damage);
                Destroy(gameObject);
            }
        }
    }

    #region Getters & Setters
    public void SetOwner(GameObject player)
    {
        Owner = player;
    }

    public GameObject GetOwner()
    {
        return Owner;
    }

    public int GetDamage()
    {
        return damage;
    }
    #endregion

    private void OnDestroy()
    {
        Quaternion _rotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
        Instantiate(Explosion, transform.position, _rotation);
    }

    private void OnDrawGizmosSelected()
    {
        if (currentTrackRadius != 0)
            Gizmos.DrawWireSphere(transform.position, currentTrackRadius);
        else
            Gizmos.DrawWireSphere(transform.position, trackRadius);
    }

}
