using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{

    bool clockWise;
    float rotationSpeed = 0.02f;
    void Start()
    {
        transform.Rotate(0, 0, 0);
        InvokeRepeating("Rotate", 3, 0.01f);
        InvokeRepeating("ChangeDirection", 5, 10f);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(clockWise);
                
    }

    public void Rotate()
    {

        if (clockWise)
        {
            transform.Rotate(0, 0, rotationSpeed);
        }
        else
        {
            transform.Rotate(0, 0, -rotationSpeed);
        }

    }

    public void ChangeDirection()
    {
        float changeChance = Random.Range(0, 2);
        if (changeChance == 0)
        {
            clockWise = !clockWise;
        }
        if (changeChance == 1)
        { 
            rotationSpeed = 0;
        }

        float randomRotation = Random.Range(-0.1f, 0.1f);
        rotationSpeed = Mathf.Lerp(rotationSpeed, randomRotation, 0.001f);
        
    }
}
