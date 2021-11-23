using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float thrust;

    Vector2 lookDirection;

    Rigidbody2D rb;
    public GameObject playerShip;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        lookDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * -1;

        Rotate();
                
        if (Input.GetButton("Fire2"))
        {
            Thrust();
        }

        if (Input.GetButton("Jump"))
        {
            Respawn();
        }
    }

    public void Rotate()
    {
        float angle = Mathf.Atan2(lookDirection.x, lookDirection.y) * Mathf.Rad2Deg;
        Debug.Log(angle);
        Quaternion newRotation = Quaternion.Euler(0, 0, 0);
        if (lookDirection != Vector2.zero)
        {
            newRotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }
        transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * 5f);

        

    }

    public void Thrust()
    {
        rb.AddForce(transform.up * thrust * Time.deltaTime, ForceMode2D.Impulse);
    }

    public void Respawn()
    {
        GameObject newShip = Instantiate(playerShip, new Vector3(0, 0, 0), Quaternion.identity);
        Destroy(gameObject);
    }

}
