using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{
    [SerializeField] PlayerMovement movement;
    [SerializeField] ShipAnimation anim;
    [SerializeField] Camera mainCam;
    [SerializeField] GameObject Shield;
    public PlayerAttack attack;
    public CapsuleCollider2D playerCollider;

    public int score = 0;

    Rigidbody2D rb;

    bool isThrust = false, isBrake = false, isAttacking = false;
    bool shielded = false, dashing = false;
    public bool invincible;

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
        //this.GetComponent<Rigidbody2D>().velocity = new Vector2(this.GetComponent<Rigidbody2D>().velocity.x,Mathf.Clamp(this.GetComponent<Rigidbody2D>().velocity.y, -8, 8));
    }

    #region Inputs
    public void Movement(InputAction.CallbackContext value)
    {

        if (movement.GetDriftMode()) { return; }
        Vector2 _dir = value.ReadValue<Vector2>();

        if (value.control.device.name == "Mouse")
        {
            _dir = mainCam.ScreenToWorldPoint(_dir);
        }

        _dir.x *= -1;
        movement.UpdateDirection(_dir);
        _dir.Normalize();
        //anim.updateRotation(GetComponent<Rigidbody2D>().velocity.x * -1);
    }

    public void Thrust(InputAction.CallbackContext value)
    {
        float _thrustPower = value.ReadValue<float>();
        if (_thrustPower >= 1) { isThrust = true; }
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
        playerDamage = 0;
    }

    public void Attack(InputAction.CallbackContext value)
    {
        if (value.ReadValue<float>() >= 1) { isAttacking = true; }
        else { isAttacking = false; }

        attack.Attack();
    }

    public void Drift(InputAction.CallbackContext value)
    {
        Vector2 _dir = value.ReadValue<Vector2>();
        _dir.x *= -1;

        if (_dir != Vector2.zero)
        {
            movement.SetDriftMode(true);
            movement.UpdateDirection(_dir);
        }
        else { movement.SetDriftMode(false); }

    }

    public void ToggleShield(InputAction.CallbackContext value)
    {
        shielded = true;
        Shield.SetActive(true);
        StartCoroutine(ShieldTime());
    }

    public void Dash(InputAction.CallbackContext value)
    {
        if (!dashing)
        {
            movement.Dash();
            dashing = true;
            StartCoroutine(DashTime());
        }
    }
    #endregion

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Projectile"))
        {
            IBullet _bullet = other.GetComponent<IBullet>();
            if (_bullet == null) { Debug.LogError("THE OBJECT DOES NOT HAVE THE BULLET INTERFACE"); return; }

            if (transform.gameObject != _bullet.GetOwner())
            {
                playerDamage += other.GetComponent<Bullet>().damage;
                Destroy(other.gameObject);
            }
        }

        if (other.CompareTag("Player"))
        {
            Rigidbody2D _otherRb = other.GetComponent<Rigidbody2D>();
            
            if (!invincible && !other.GetComponent<PlayerController>().invincible)
            CollisionHandler.DoCollision(rb, _otherRb);
            
            invincible = true;
            StartCoroutine(InvinceTime());
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

    IEnumerator ShieldTime()
    {
        yield return new WaitForSeconds(1);
        Shield.SetActive(false);
        shielded = false;
    }

    IEnumerator DashTime()
    {
        yield return new WaitForSeconds(1);
        dashing = false;
    }

    IEnumerator InvinceTime()
    {
        yield return new WaitForSeconds(0.2f);
        invincible = false;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            invincible = false;
        }
    }

}
