using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    float rotationSpeed;
    bool changeDirection;

    // Start is called before the first frame update
    void Start()
    {
        
        transform.Rotate(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        Invoke("RotateCamera", 3f);
         

    }

    public void RotateCamera()
    {
             transform.Rotate(0, 0, 0.01f);
    }
}
