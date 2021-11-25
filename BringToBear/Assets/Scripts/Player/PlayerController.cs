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
        Debug.Log(playerDamage);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Projectile"))
        {
            playerDamage += other.GetComponent<Bullet>().damage;
            Destroy(other.gameObject);
        }
    }





}
