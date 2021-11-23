using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Vector2 lookDirection;
        
        void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        lookDirection = new Vector2 (Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        Rotate();
    }

    public void Rotate()
    {
        float angle = Mathf.Atan2(lookDirection.x, lookDirection.y) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));        
        //Debug.Log(lookAngle);
    }


}
