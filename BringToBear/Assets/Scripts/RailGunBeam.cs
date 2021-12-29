using System.Collections.Generic;
using UnityEngine;

public class RailGunBeam : MonoBehaviour, IBullet
{
    public float speed;
    public float force;
    public int damage;
    public GameObject Explosion;
    Rigidbody2D rb;

    public GameObject Owner { get; set; }
    public GameObject OwnerShield { get; set; }

    void Start()
    {
        
    }



    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Projectile")) { return; }

        if (other.gameObject != Owner)
        {
            if (other.GetComponent<ICharacter>() != null)
            {
                other.GetComponent<ICharacter>().Damage(damage);
                Instantiate(Explosion, other.transform.position, Random.rotation);
                Destroy(gameObject);
            }
        }

        if (other.gameObject.CompareTag("Shield") && other.transform.parent.gameObject != Owner)
        {
            Owner = other.transform.parent.gameObject;
        }

    }

    private void OnDestroy()
    {
        Quaternion _rotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
        
    }


}
