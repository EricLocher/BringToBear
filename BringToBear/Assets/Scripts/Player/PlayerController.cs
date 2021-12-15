using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour, ICharacter
{
    [SerializeField] PlayerMovement movement;
    [SerializeField] ShipAnimation anim;
    [SerializeField] Camera mainCam;
    [SerializeField] GameObject Shield;
    [SerializeField] GameObject Coin;
    [SerializeField] PlayerAttack attack;
    [SerializeField] PlayerDash dash;

    public GameObject dashAnimation;
    public SpriteRenderer dashRenderer;
    
    public bool invincible;
    public bool shielded;
    Rigidbody2D rb;

    public float playerDamage = 0;
    public float shieldForce;
    public int coinsOnPlayer = 0;
    public int coinsOnMamaBear = 0;

    bool isThrust = false, isBrake = false, isAttacking = false;
    bool dashCoolDown = false, dashButton = false;

    public int amountOfDashes = 3;

    private void Start()
    {
        mainCam = GameObject.Find("Main Camera").GetComponent<Camera>();
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(ReloadDashes());
    }

    void Update()
    {

        if (dash.dashing)
        {
            dashAnimation.SetActive(true);
            dashRenderer.flipX = !dashRenderer.flipX;

        }
        else
            dashAnimation.SetActive(false);

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
        if (dash.dashing) { return; }

        if (movement.GetDriftMode()) { return; }
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
        res();
    }

    public void res()
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
        if (!shielded)
        {
            shielded = true;
            Shield.SetActive(true);
            StartCoroutine(ShieldTime());
        }
    }

    public void Dash(InputAction.CallbackContext value)
    {
        if (amountOfDashes <= 0) { return; }

        if (dashButton)
        {
            if (value.ReadValue<float>() < 0.5)
                dashButton = false;

            return;
        }

        dashButton = true;
        amountOfDashes--;

        dash.Dash();
        dashCoolDown = true;


    }
    public void DropHoney(InputAction.CallbackContext value)
    {
        GameObject _coin = Instantiate(Coin, transform.position, Quaternion.identity);
        _coin.GetComponent<CoinsPickup>().owner = this;
    }
    #endregion

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Shield"))
        {
            Rigidbody2D _otherRb = other.GetComponent<Rigidbody2D>();

            if (!invincible && !other.GetComponent<PlayerController>().invincible)
            {
                if (shielded)
                    shieldForce = 2;
                else
                    shieldForce = 1;
                CollisionHandler.DoCollision(rb, _otherRb, shieldForce);
                invincible = true;
                StartCoroutine(InvinceTime());
            }
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
        yield return new WaitForSeconds(1);
        shielded = false;
    }

    IEnumerator DashTime()
    {
        yield return new WaitForSeconds(1);
        dashCoolDown = false;
    }

    IEnumerator InvinceTime()
    {
        yield return new WaitForSeconds(0.2f);
        invincible = false;
    }

    IEnumerator ReloadDashes()
    {
        yield return new WaitForSeconds(1.5f);
        if (amountOfDashes <= 3)
            amountOfDashes++;
        StartCoroutine(ReloadDashes());
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            invincible = false;
        }
    }

    public void Damage(int amount)
    {
        if (!shielded)
            playerDamage += amount;
    }
}
