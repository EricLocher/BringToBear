using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    float thrust = 200;
    float turnSpeed;
    public float stabilizeSpeed;
    
    public bool boost;
    bool driftMode;

    float angle;

    Vector2 lookDirection;
    Rigidbody2D rb;
    public GameObject playerShip;


    void Start()
    {
        turnSpeed = 5;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        driftMode = false;

        lookDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * -1;

        Rotate();


        if (Input.GetButton("Fire2") || Input.GetAxis("R2") > 0)
        {
            Thrust();
        }

        if (Input.GetAxis("L2") > 0)
        {
            Brake();
        }
                     

        if (Input.GetButton("Jump"))
        {
            Respawn();
        }

        if (Input.GetButton("Fire3"))
        {

            driftMode = true;
        }


        if (driftMode)
        {
            turnSpeed = 10;
        }
        else
        {
            turnSpeed = 3;
            Stabilize();
        }

    }

    public void Rotate()
    {

        angle = Mathf.Atan2(lookDirection.x, lookDirection.y) * Mathf.Rad2Deg;
        Quaternion newRotation = Quaternion.Euler(0, 0, 0);
        if (lookDirection != Vector2.zero)
        {
            newRotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }
        transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * turnSpeed);
    }

    public void Thrust()
    {
        if (Input.GetAxis("R2") > 0)
        {
            thrust = 300;
        }
        else
            thrust = 200;

        rb.AddForce(transform.up * thrust * Time.deltaTime, ForceMode2D.Impulse);
    }

    private void Brake()
    {
        Debug.Log("Braking!");

        
        rb.AddForce(Vector3.up * -100 * Time.deltaTime, ForceMode2D.Impulse);
    }


    public void Respawn()
    {
        GameObject newShip = Instantiate(playerShip, new Vector3(0, 0, 0), Quaternion.identity);
        Destroy(gameObject);
    }

    private void Stabilize()
    {
        if (angle == 0 || angle == 180 || angle == -180)
        {
            return;
        }
        else if ((angle < -90 && angle > -180) || (angle > 90 && angle < 180))
        {
            return;
        }

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
