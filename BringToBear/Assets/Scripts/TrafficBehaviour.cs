using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficBehaviour : MonoBehaviour
{
    Rigidbody2D rb;
    public float trafficThrust = 1;
    public float maxVel = 25;
    public SpriteRenderer spriteRenderer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 2;
    }

    void Update()
    {
        Debug.Log(rb.transform.eulerAngles.z);
        Stabilize();
        GravityAdjuster();
        rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxVel);
        rb.AddForce(transform.up * trafficThrust);

        if (transform.position.y < -110 || transform.position.y > 140 || Mathf.Abs(transform.position.x) > 50)
        {
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
    }

    private void OnCollisionEnter2D(Collision2D other)
    {

        

        if (other.gameObject.CompareTag("Player"))
        {
            trafficThrust = 150f;
            maxVel = 55;
        }
        
            
    }

    private void Stabilize()
    {
        Quaternion _newRotation = Quaternion.Euler(0, 0, 0);
        transform.rotation = Quaternion.Slerp(transform.rotation, _newRotation, Time.deltaTime * 2);

        rb.angularVelocity = Mathf.Lerp(rb.angularVelocity, 0, 1 * Time.deltaTime);

    }

    private void GravityAdjuster()
    {
        if (Mathf.Abs(transform.position.x - spriteRenderer.bounds.center.x) > spriteRenderer.bounds.extents.x)
        {
            rb.gravityScale += Time.deltaTime * 100;
        }
    }
}



