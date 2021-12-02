using System;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{
    [SerializeField] PlayerMovement movement;
    public PlayerAttack attack;
    [SerializeField] ShipAnimation anim;
    [SerializeField] Camera mainCam;

    public int score = 0;

    Rigidbody2D rb;
        
    bool isThrust = false, isBrake = false, isAttacking = false;
    public float playerDamage = 0;

    private void Start()
    {
        mainCam = GameObject.Find("Main Camera").GetComponent<Camera>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (isThrust)
            movement.Thrust(1);

        if (isBrake)
            movement.Brake(1);

        if (isAttacking)
            attack.Attack();
    }


    #region Inputs
    public void Movement(InputAction.CallbackContext value)
    {  

        if(movement.GetDriftMode()) { return; }
        Vector2 _dir = value.ReadValue<Vector2>();

        if (value.control.device.name == "Mouse")
        {
            _dir = mainCam.ScreenToWorldPoint(_dir);
        }

        _dir.x *= -1;
        movement.UpdateDirection(_dir);
        _dir.Normalize();
        anim.updateRotation(GetComponent<Rigidbody2D>().velocity.x * -1);
    }

    public void Thrust(InputAction.CallbackContext value)
    {
        float _thrustPower = value.ReadValue<float>();
        if(_thrustPower >= 1) { isThrust = true; } 
        else { isThrust = false; }

        movement.Thrust(_thrustPower);
    }

    public void Brake(InputAction.CallbackContext value)
    {
        
        float _brakePower = value.ReadValue<float>();
        if (_brakePower >= 1) { isBrake = true; }
        else { isBrake = false; }

        movement.Brake(_brakePower);
    }

    public void Respawn(InputAction.CallbackContext value)
    {
        transform.position = new Vector3(-10, 0, 0);
        GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        GetComponent<Rigidbody2D>().angularVelocity = 0;
        transform.rotation = Quaternion.Euler(Vector3.zero);
    }

    public void Attack(InputAction.CallbackContext value)
    {
        if(value.ReadValue<float>() >= 1) { isAttacking = true; }
        else { isAttacking = false; }

        attack.Attack();
    }

    public void Drift(InputAction.CallbackContext value)
    {
        Vector2 _dir = value.ReadValue<Vector2>();
        _dir.x *= -1;

        if (_dir != Vector2.zero) { 
            movement.SetDriftMode(true);
            movement.UpdateDirection(_dir);
        }
        else { movement.SetDriftMode(false); }
          
    }
    #endregion

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
        if (other.CompareTag("Player"))
        {
            Rigidbody2D _otherRb = other.GetComponent<Rigidbody2D>();
            CollisionHandler.DoCollision(rb, _otherRb);
        }

        else
        {
            if (other.GetComponent<IInteractable>() != null)
            {
                IInteractable _interactable = other.GetComponent<IInteractable>();
                _interactable.Interact(this);
            }
        }
    }
}
