using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    PlayerMovement movement;
    bool isThrust = false;


    float playerDamage = 0;

    void Start()
    {
        
    }

    void Update()
    {
        if (isThrust)
            movement.UpdateThrust(1);
    }

    public void Movement(InputAction.CallbackContext value)
    {
        Vector2 _dir = value.ReadValue<Vector2>();
        _dir.x *= -1;
        movement.UpdateDirection(_dir);
    }

    public void Thrust(InputAction.CallbackContext value)
    {
        float thrustPower = value.ReadValue<float>();
        if(thrustPower >= 1) { isThrust = true; } 
        else { isThrust = false; }

        movement.UpdateThrust(thrustPower);
    }

    public void Respawn(InputAction.CallbackContext value)
    {
        transform.position = new Vector3(-10, 0, 0);
        GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        GetComponent<Rigidbody2D>().angularVelocity = 0;
        transform.rotation = Quaternion.Euler(Vector3.zero);
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
