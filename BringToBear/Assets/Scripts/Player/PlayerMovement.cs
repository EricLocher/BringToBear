using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    float thrust = 200;
    float turnSpeed;

    public float stabilizeSpeed;

    public PlayerControls input;

    public bool boost;
    bool driftMode;

    float angle;
    float cameraAngle;
    
    public Vector2 lookDirection;
    Rigidbody2D rb;
    public GameObject playerShip;
    public GameObject background;


    void Start()
    {
        turnSpeed = 5;
        rb = GetComponent<Rigidbody2D>();
        input = new PlayerControls();
        lookDirection = Vector2.zero;
    }

    // Update is called once per frame
    void Update()
    {
        driftMode = false;

        Rotate();

        //if (Input.GetAxis("L2") > 0)
        //{
        //    Brake();
        //}


        //if (Input.GetButton("Jump"))
        //{
        //    Respawn();
        //}

        //if (Input.GetButton("Fire3"))
        //{

        //    driftMode = true;
        //}


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

        GravityAdjuster();

    }

    public void Rotate()
    {

        //TODO: Fix issue where direction gets screwy. Seems to be an issue where we need to differentiate between -180 / 180 etc.

        angle = Mathf.Atan2(lookDirection.x, lookDirection.y) * Mathf.Rad2Deg;
        Quaternion newRotation = Quaternion.Euler(0, 0, 0);
        if (lookDirection != Vector2.zero)
        {
            newRotation = Quaternion.Euler(new Vector3(0, 0, angle + cameraAngle));
        }
        transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * turnSpeed);
    }

    public void UpdateDirection(Vector2 dir)
    {
        lookDirection = dir;
        Debug.Log(lookDirection);
        Rotate();
    }

    public void UpdateThrust(float thrustPower)
    {
        Thrust(thrustPower);
    }

    private void Thrust(float thrustPower)
    {

        if (thrustPower > 0)
        {
            thrust = 300 * thrustPower;
        }
        else
            thrust = 200;

        rb.AddForce(transform.up * thrust * Time.deltaTime, ForceMode2D.Impulse);
    }

    private void Brake()
    {
        //rb.AddForce(Vector3.up * -100 * Time.deltaTime, ForceMode2D.Impulse);
        rb.AddForce(-rb.velocity * 5);        
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
    
    public void GravityAdjuster()
    {
        Bounds _bg = background.GetComponent<SpriteRenderer>().bounds;
        float _Posx = transform.position.x - _bg.center.x;


        if (Mathf.Abs(_Posx) > _bg.extents.x)
        {

            if (rb.gravityScale < 6)
                rb.gravityScale = 6;
            else
                rb.gravityScale += Time.deltaTime;
        }
        //TODO: Make this dynamic to the viewport rather than hard-coded y-values
        else if (transform.position.y < -12)
        {
            if (rb.gravityScale < 1)
                rb.gravityScale = 1;
            else
                rb.gravityScale -= Time.deltaTime * 2;
        }
        else if (transform.position.y > 9)
        {
            if (rb.gravityScale > 6)
                rb.gravityScale = 6;
            else
                rb.gravityScale += Time.deltaTime * 10;
        }
        else
        {
            rb.gravityScale = 3;
        }
    }



}
