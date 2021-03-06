using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public GameObject playerShip;
    public Rigidbody2D rb;
    public float stabilizeSpeed;
    public float thrust = 200;
    public float turnSpeed = 5;

    public Vector2 lookDirection;
    public bool boost;
    bool driftMode;
    float angle;
    float cameraAngle;


    void Start()
    {
        lookDirection = Vector2.zero;
    }

    void Update()
    {
        driftMode = false;
        Rotate();

        if (driftMode)
        {
            turnSpeed = 10;
            cameraAngle = Camera.main.transform.eulerAngles.z;
        }
        else
        {
            turnSpeed = 3;
            cameraAngle = 0;
            Stabilize();
        }

    }

    public void Rotate()
    {
        angle = Mathf.Atan2(lookDirection.x, lookDirection.y) * Mathf.Rad2Deg;

        Quaternion _newRotation = Quaternion.Euler(0, 0, 0);
        if (lookDirection != Vector2.zero)
        {
            _newRotation = Quaternion.Euler(new Vector3(0, 0, angle + cameraAngle));
        }
        transform.rotation = Quaternion.Slerp(transform.rotation, _newRotation, Time.deltaTime * turnSpeed);
    }

    public void UpdateDirection(Vector2 dir)
    {
        lookDirection = dir;
        Rotate();
    }

    public void SetDriftMode(bool state)
    {
        driftMode = state;
    }

    public bool GetDriftMode()
    {
        return driftMode;
    }

    public void Thrust(float thrustPower)
    {
        if (thrustPower > 0) { thrust = 400 * thrustPower; }
        //rb.AddForce(transform.up * thrust * Time.deltaTime, ForceMode2D.Impulse);

        Vector2 forceToAdd = transform.up * thrust * Time.deltaTime;
        if (Mathf.Abs(rb.velocity.x + forceToAdd.x) < 75)
        {
            rb.AddForce(new Vector2(forceToAdd.x, 0), ForceMode2D.Impulse);
        }

        if (Mathf.Abs(rb.velocity.y + forceToAdd.y) < 75)
        {
            rb.AddForce(new Vector2(0, forceToAdd.y), ForceMode2D.Impulse);
        }
    }

    public void Brake(float brakePower)
    {
        rb.AddForce(-rb.velocity * (0.5f * brakePower));
    }

    public void Respawn()
    {
        transform.position = new Vector3(-10, 0, 0);
        GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        GetComponent<Rigidbody2D>().angularVelocity = 0;
        transform.rotation = Quaternion.Euler(Vector3.zero);
    }

    private void Stabilize()
    {
        if (angle == 0 || angle == 180 || angle == -180) { return; }
        else if ((angle < -90 && angle > -180) || (angle > 90 && angle < 180)) { return; }

        Vector2 stabilizeVector = Vector2.zero;
        Vector2 vel = rb.velocity.normalized;

        if (angle > 0 && vel != Vector2.zero)
        {
            stabilizeVector = Vector2.left;
        }
        else if (angle < 0 && vel != Vector2.zero)
        {
            stabilizeVector = Vector2.right;
        }

        stabilizeVector = (stabilizeVector).normalized;
        rb.AddForce(stabilizeVector * stabilizeSpeed * Time.deltaTime, ForceMode2D.Impulse);

    }

}
