using System.Collections.Generic;
using UnityEngine;

public class RailGunBeam : MonoBehaviour, IBullet
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
        
    }



    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Projectile")) { return; }

        if (other.gameObject != Owner)
        {
            if (other.GetComponent<ICharacter>() != null)
            {
                other.GetComponent<ICharacter>().Damage(damage);
                GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>().Shake(cameraShake);
                Quaternion _rotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
                Instantiate(Explosion, other.transform.position, _rotation);
                Destroy(gameObject);
            }
        }

        if (other.gameObject.CompareTag("Shield") && other.transform.parent.gameObject != Owner)
        {
            Owner = other.transform.parent.gameObject;
        }

    }
}
