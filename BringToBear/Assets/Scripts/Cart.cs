using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Cart : MonoBehaviour, ICharacter
{

    [SerializeField] Vector2 repulsionField;

    public Cart connectedCart;
    public int cartHP = 100;
    public float cameraShake;
    public GameObject explosion;
    public List<GameObject> Pickups;
    public GameObject HitIndicator;
    private GameObject anchor;


    private void Start()
    {
        anchor = transform.GetChild(0).gameObject;
    }

    private void Update()
    {
        if (cartHP <= 0)
        {
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>().Shake(cameraShake);
            transform.parent.GetComponent<Train>().DestroyedCart(gameObject);
            int selectedPickup = Random.Range(0, Pickups.Count - 1);

            for (int i = 0; i < 25; i++)
            {
                Instantiate(Pickups[0], transform.position, Quaternion.identity);
               
            }

            for (int i = 0; i < 6; i++)
            {
                Instantiate(explosion, new Vector2(transform.position.x + Random.Range(4, 14),
                                                   transform.position.y + Random.Range(4, 14)), Random.rotation);
            }
            Destroy(gameObject);
        }

        LookAt();
        Move();
        Repulsion();
    }

    void Move()
    {
        if (connectedCart == null) { return; }

        Vector2 _cartPos = connectedCart.transform.position;

        if (transform.position.x != _cartPos.x)
        {
            transform.position = new Vector2(Mathf.Lerp(transform.position.x, _cartPos.x, 0.05f), transform.position.y);
        }

  
    }
    void LookAt()
    {
        if (connectedCart == null) { return; }
  
        try
        {
            Vector2 _cartAnchor = connectedCart.anchor.transform.position;
            Vector2 diff = _cartAnchor - (Vector2)transform.position;
            diff.Normalize();

            float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
        }
        catch (System.Exception e)
        {
            return;
        }
    }

    void Repulsion()
    {
        foreach (PlayerController player in GameController.Players)
        {
            Vector2 _playerPos = player.transform.position;
            Vector2 _TopLeft = (Vector2)transform.position + new Vector2(-repulsionField.x, repulsionField.y) / 2;

            if(
                _playerPos.x > _TopLeft.x && _playerPos.x < (_TopLeft.x + repulsionField.x)
                &&
                _playerPos.y < _TopLeft.y && _playerPos.y > (_TopLeft.y - repulsionField.y)
              )
            {
                Vector2 _force = (player.transform.position - transform.position) * 1.2f;
                _force = Vector2.ClampMagnitude(_force, 4f);

                player.GetComponent<Rigidbody2D>().AddForce(_force, ForceMode2D.Impulse);
            }
        }
    }

    public void Damage(int amount)
    {
        cartHP = cartHP - amount;
        int dropTable = Random.Range(0, 300);
        HitIndicator.gameObject.SetActive(true);

        if (dropTable < 70)
        {
            Instantiate(Pickups[0], transform.position, Quaternion.identity);
        }
        if (dropTable > 35 && dropTable < 38)
        {
            int _weapon = Random.Range(0, 3);

            switch (_weapon)
            {
                case 0:
                    Instantiate(Pickups[1], transform.position, Quaternion.identity);
                    break;
                case 1:
                    Instantiate(Pickups[2], transform.position, Quaternion.identity);
                    break;
                case 2:
                    Instantiate(Pickups[3], transform.position, Quaternion.identity);
                    break;
               
                default:
                    break;
            }

        }

    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, repulsionField);
    }

}
