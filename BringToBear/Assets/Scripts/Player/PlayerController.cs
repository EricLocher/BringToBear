using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour, ICharacter
{
    [Header("Reference")]
    [SerializeField] PlayerInput playerInput = null;
    //[SerializeField] private PlayerController playerController = null;


    [Header("Player Components")]
    [SerializeField] PlayerMovement movement;
    [SerializeField] ShipAnimation anim;
    [SerializeField] GameObject Shield;
    [SerializeField] GameObject PlayerCoin;
    [SerializeField] GameObject droppedCoin;
    [SerializeField] PlayerAttack attack;
    [SerializeField] PlayerDash dash;
    [SerializeField] Gradient healthIndicator;

    [SerializeField] Camera mainCam;

    public GameObject dashAnimation;
    public SpriteRenderer dashRenderer;
    public SpriteRenderer playerOutline;
    public GameObject HitIndicator;
    public GameObject KOCoin;

    public bool invincible;
    public bool shielded;
    public PlayerState state;

    Rigidbody2D rb;
    AudioSource audioSource;
    public AudioClip[] boom;
    public AudioClip[] dashSound;
    public GameObject explosion;

    public float damageTaken = 0;
    public float shieldForce;
    public int coinsOnPlayer = 0;
    public int coinsDeposited = 0;

    bool isThrust = false, isBrake = false, isAttacking = false;
    bool dashButton = false;

    public int amountOfDashes = 3;

    public Gun machinegun;
    public Gun railgun;
    public Gun minigun;
    public Gun broadside;
    public Gun missile;

    public PlayerInput PlayerInput => playerInput;


    private void Start()
    {
        mainCam = GameObject.Find("Main Camera").GetComponent<Camera>();
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(ReloadDashes());
        state = PlayerState.Alive;
    }

    void Update()
    {
        //playerOutline.color = healthIndicator.Evaluate(damageTaken / 2000);
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

        if (damageTaken >= 2000 && state != PlayerState.Dead)
        {

            for (int i = 0; i < 6; i++)
            {
                Instantiate(explosion, new Vector2(transform.position.x + Random.Range(4, 14),
                                                   transform.position.y + Random.Range(4, 14)), Random.rotation);

            }
            DamageRespawn();
        }
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
        _dir.Normalize();
        movement.UpdateDirection(_dir);
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

    public void DamageRespawn()
    {
        state = PlayerState.Dead;
        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(false);
        for (int i = 0; i < coinsOnPlayer / 5; i++)
        {
            GameObject _coin = Instantiate(PlayerCoin, transform.position, Quaternion.identity);
            _coin.GetComponent<PlayerCoin>().score = 5;
        }

        StartCoroutine(RespawnTimer());
    }

    public void KoRespawn()
    {
        state = PlayerState.Dead;

        for (int i = 0; i < coinsOnPlayer / 5; i++)
        {
            GameObject _coin = Instantiate(KOCoin, transform.position, Quaternion.identity);
            _coin.GetComponent<KOCoin>().score = 5;
        }

        StartCoroutine(RespawnTimer());
    }
    public void Attack(InputAction.CallbackContext value)
    {
        Debug.Log(value.ReadValue<float>());
        if (value.ReadValue<float>() >= 1) { isAttacking = true; }
        else { isAttacking = false; }

        attack.Attack();
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

        switch (amountOfDashes)
        {
            case 3:
                audioSource.PlayOneShot(dashSound[0], 0.5f);
                break;
            case 2:
                audioSource.PlayOneShot(dashSound[1], 0.4f);
                break;
            case 1:
                audioSource.PlayOneShot(dashSound[2], 0.2f);
                break;
            default:
                break;
        }

        amountOfDashes--;

        dash.Dash();

    }
    public void DropHoney(InputAction.CallbackContext value)
    {
        if (coinsOnPlayer <= 0) { return; }
        rb.AddForce(Vector2.up * 1.6f, ForceMode2D.Impulse);
        GameObject _coin = Instantiate(PlayerCoin, transform.position, Quaternion.identity);
        _coin.GetComponent<PlayerCoin>().owner = this;
        _coin.GetComponent<PlayerCoin>().score = 1;
        coinsOnPlayer -= 1;
    }

    public void DropWeapon(InputAction.CallbackContext value)
    {
        GetComponent<PlayerAttack>().SetWeapon(machinegun);
    }

    public void SelectRailgun(InputAction.CallbackContext value)
    {
        GetComponent<PlayerAttack>().SetWeapon(railgun);
    }

    public void SelectMinigun(InputAction.CallbackContext value)
    {
        GetComponent<PlayerAttack>().SetWeapon(minigun);
    }

    public void SelectBroadside(InputAction.CallbackContext value)
    {
        GetComponent<PlayerAttack>().SetWeapon(broadside);
    }

    public void SelectMissile(InputAction.CallbackContext value)
    {
        GetComponent<PlayerAttack>().SetWeapon(missile);
    }

    #endregion

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Player") || other.CompareTag("Shield"))
        {

            PlayerController _playerController = other.GetComponent<PlayerController>();


            if (other.CompareTag("Shield"))
            {
                _playerController = other.GetComponentInParent<PlayerController>();
            }
            Rigidbody2D _otherRb = _playerController.GetComponent<Rigidbody2D>();

            if (!invincible && !_playerController.invincible)
            {
                if (shielded)
                    shieldForce = 2;
                else
                    shieldForce = 1;
                CollisionHandler.DoCollision(rb, _otherRb, shieldForce, other.ClosestPoint(transform.position));
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

    IEnumerator InvinceTime()
    {
        yield return new WaitForSeconds(0.2f);
        invincible = false;
    }

    IEnumerator ReloadDashes()
    {
        yield return new WaitForSeconds(1.5f);
        if (amountOfDashes < 3)
            amountOfDashes++;
        StartCoroutine(ReloadDashes());
    }

    IEnumerator RespawnTimer()
    {
        yield return new WaitForSeconds(1);
        transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, 0);
        GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        GetComponent<Rigidbody2D>().angularVelocity = 0;
        transform.rotation = Quaternion.Euler(Vector3.zero);
        coinsOnPlayer = 0;
        damageTaken = 0;
        GetComponent<PlayerAttack>().SetWeapon(machinegun);
        GetComponent<PlayerAttack>().SetWeapon(machinegun);
        state = PlayerState.Alive;
        transform.GetChild(0).gameObject.SetActive(true);
        transform.GetChild(1).gameObject.SetActive(true);
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
        {
            damageTaken += amount;
            audioSource.PlayOneShot(boom[Random.Range(0, boom.Length)], 0.5f);
            HitIndicator.gameObject.SetActive(true);
        }
        anim.Hit();
    }

}

public enum PlayerState
{
    Alive,
    Dead
}