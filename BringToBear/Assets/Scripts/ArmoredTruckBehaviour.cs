using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmoredTruckBehaviour : MonoBehaviour, ICharacter
{
    Rigidbody2D rb;

    public float trafficThrust;
    public float maxVel = 35;
    public float truckStartHP;
    public GameObject explosion;
    public SpriteRenderer spriteRenderer;
    public List<GameObject> Pickups;
    public GameObject HitIndicator;


    public float truckHP;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 2;

        truckHP = truckStartHP;
    }

    void Update()
    {
        trafficThrust = Random.Range(80, 100);

        Stabilize();
        GravityAdjuster();
        rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxVel);
        rb.AddForce(transform.up * trafficThrust);

        if (transform.position.y < -200 || transform.position.y > 300 || Mathf.Abs(transform.position.x) > 140)
        {
            ResetMe();
        }

        if (truckHP <= 0)
        {
            Instantiate(Pickups[1], transform.position, Quaternion.identity);
            for (int i = 0; i < 25; i++)
            {
                Instantiate(Pickups[0], transform.position, Quaternion.identity);

            }
            for (int i = 0; i < 7; i++)
            {
                Instantiate(explosion, new Vector2(transform.position.x + Random.Range(1, 5),
                                                   transform.position.y + Random.Range(1, 5)), Random.rotation);
            }
            ResetMe();


        }
    }

    private void ResetMe()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = 0;
        transform.rotation = Quaternion.Euler(Vector3.zero);
        Vector3 newPos = TrafficController.INSTANCE.GetFreeVehiclePosition(0);
        transform.position = newPos;
        trafficThrust = 1;
        maxVel = 25;
        rb.gravityScale = 2;
        truckHP = truckStartHP;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Panic();
        }

    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Mama Bear"))
        {
            trafficThrust = 0;
            rb.AddForce((new Vector2(Random.Range(-3, 4), 3)) * 50, ForceMode2D.Impulse);
            rb.AddTorque(Random.Range(-50, 50) * 50, ForceMode2D.Impulse);
            Damage(50);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Mandible"))
        {
            trafficThrust = 0;
            rb.AddForce((new Vector2(Random.Range(-3, 4), 3)) * 50, ForceMode2D.Impulse);
            rb.AddTorque(Random.Range(-50, 50) * 50, ForceMode2D.Impulse);
            Damage(50);
        }
    }

    private void Panic()
    {
        trafficThrust = 150;
        maxVel = 55;
    }

    private void Stabilize()
    {
        Quaternion _newRotation = Quaternion.Euler(0, 0, 0);
        transform.rotation = Quaternion.Slerp(transform.rotation, _newRotation, Time.deltaTime * 16);

        rb.angularVelocity = Mathf.Lerp(rb.angularVelocity, 0, 1 * Time.deltaTime);
    }

    private void GravityAdjuster()
    {
        if (Mathf.Abs(transform.position.x - spriteRenderer.bounds.center.x) > spriteRenderer.bounds.extents.x)
        {
            rb.gravityScale += Time.deltaTime * 100;
        }
    }

    public void Damage(int amount)
    {
        truckHP -= amount;
        rb.angularVelocity = Random.Range(-200, 200);
        HitIndicator.gameObject.SetActive(true);


        int dropTable = Random.Range(0, 100);

        if (dropTable < 5)
        {
            Panic();
        }
    }

}