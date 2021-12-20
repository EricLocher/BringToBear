using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficBehaviour : MonoBehaviour, ICharacter
{
    Rigidbody2D rb;
    
    public float trafficThrust;
    public float maxVel = 35;
    public float truckStartHP;
    public GameObject explosion;
    public SpriteRenderer spriteRenderer;
    public List<GameObject> Pickups;

    public float truckHP;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 2;
        trafficThrust = Random.Range(10, 45);
        truckHP = truckStartHP;
    }

    void Update()
    {
        Stabilize();
        GravityAdjuster();
        rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxVel);
        rb.AddForce(transform.up * trafficThrust);

        if (transform.position.y < -200 || transform.position.y > 300 || Mathf.Abs(transform.position.x) > 70)
        {
            ResetMe();
        }

        if (truckHP <= 0)
        {
            RollForWeapon();
            for (int i = 0; i < 7; i++)
            {
                Instantiate(explosion, new Vector2(transform.position.x + Random.Range(1, 3),
                                                   transform.position.y + Random.Range(1, 3)), Random.rotation);
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

    private void Panic()
    {
        trafficThrust = 150f;
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
        
        int dropTable = Random.Range(0, 100);

        if (dropTable < 20)
        {
            Panic();
        }
        else if (dropTable == 21)
            RollForWeapon();


    }

    private void RollForWeapon()
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



