using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour, IBullet
{
    public float speed;
    public int damage;
    public GameObject Explosion;
    Rigidbody2D rb;

    public GameObject Owner { get; set; }
    public GameObject OwnerShield { get; set; }

    void Start()
    {
        rb = transform.GetComponent<Rigidbody2D>();
        rb.velocity = transform.up * speed;
    }

    void Update()
    {
        foreach (PlayerController _player in GameController.Players)
        {
            if (_player == Owner || _player == OwnerShield) { continue; }
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
        
        if (other.gameObject.CompareTag("Shield") && other.gameObject != OwnerShield)
            rb.velocity = -rb.velocity;
        Owner = other.gameObject;
        OwnerShield = other.transform.GetChild(1).gameObject;






    }

    private void OnDestroy()
    {
        Quaternion _rotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
        Instantiate(Explosion, transform.position, _rotation);
    }


}
