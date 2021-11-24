using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficBehaviour : MonoBehaviour
{
    Rigidbody2D rb;
    public float trafficThrust = 1;
    public float maxVel = 25;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 2;
               
    }

    // Update is called once per frame
    void Update()
    {
        //transform.Translate(0, -0.02f, 0);
        rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxVel);
        rb.AddForce(transform.up * trafficThrust);
        
        if (transform.position.y < -50 || Mathf.Abs(transform.position.x) > 50)
        {
            ResetMe();
        }
    }

    private void ResetMe()
    {
        GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        GetComponent<Rigidbody2D>().angularVelocity = 0;
        transform.rotation = Quaternion.Euler(Vector3.zero);
        Vector3 newPos = TrafficController.INSTANCE.GetFreeVehiclePosition(0);
        transform.position = newPos;
        maxVel = 25;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            trafficThrust = 4f;
            maxVel = 55;
        }
    }
}



