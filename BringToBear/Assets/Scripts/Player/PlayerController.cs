using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //[SerializeField]
    //PlayerMovement movement;


    float playerDamage = 0;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Projectile"))
        {
            IBullet _bullet = other.GetComponent<IBullet>();
            if(_bullet == null) { Debug.LogError("THE OBJECT DOES NOT HAVE THE BULLET INTERFACE");  return; }

            if (transform.gameObject != _bullet.GetOwner())
            {
                playerDamage += other.GetComponent<Bullet>().damage;
                Destroy(other.gameObject);
            }
        }
    }





}
