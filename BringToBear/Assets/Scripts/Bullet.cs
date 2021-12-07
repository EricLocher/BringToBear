using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour, IBullet
{
    public float speed;
    public int damage;
    public GameObject Explosion;

    GameObject Owner;
    List<GameObject> Players = new List<GameObject>();

    public void SetOwner(GameObject player)
    {
        Owner = player;
    }

    void Start()
    {
        transform.GetComponent<Rigidbody2D>().velocity = transform.up * speed;
        GameObject[] _players = GameObject.FindGameObjectsWithTag("Player");
    }

    void Update()
    {
        foreach (GameObject _player in Players)
        {
            if (_player == Owner) { continue; }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Projectile")) { return; }


        if (other.gameObject != Owner)
        {
            if (other.GetComponent<ICharacter>() != null)
            {
                other.GetComponent<ICharacter>().Damage(damage);
                Destroy(gameObject);
            }
        }
    }

    public int GetDamage()
    {
        return damage;
    }

    public GameObject GetOwner()
    {
        return Owner;
    }

    private void OnDestroy()
    {
        Quaternion _rotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
        Instantiate(Explosion, transform.position, _rotation);
    }
}
