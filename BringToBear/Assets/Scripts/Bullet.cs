using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour, IBullet
{
    public float speed;
    public float force;
    public float cameraShake;
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


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Projectile")) { return; }

        if (other.gameObject != Owner)
        {
            if (other.GetComponent<ICharacter>() != null)
            {
                if (other.CompareTag("Player"))
                {
                    other.GetComponent<Rigidbody2D>().AddForce(rb.velocity * force * other.GetComponent<PlayerController>().damageTaken / 2000);
                }
                
                other.GetComponent<ICharacter>().Damage(damage);
                GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>().Shake(cameraShake);
                Destroy(gameObject);
            }
        }

        if (other.gameObject.CompareTag("Shield") && other.transform.parent.parent.gameObject != Owner)
        {
            rb.velocity = -rb.velocity;
            Owner = other.transform.parent.gameObject;
        }

    }

    private void OnDestroy()
    {
        if (GameController.gameState == GameStates.GameOver) { return; }
        else
        {
            Quaternion _rotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
            Instantiate(Explosion, transform.position, _rotation);
        }
    }


}
