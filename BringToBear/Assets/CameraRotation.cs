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
        InvokeRepeating("ChangeDirection", 5, 3f);
    }

    // Update is called once per frame
    void Update()
    {
                       
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

        Debug.Log("Flip");
        float changeChance = Random.Range(0, 3);
        if (changeChance == 0)
        {
            Debug.Log("Change");
            clockWise = !clockWise;
            float randomRotation = Random.Range(-1f, 1f);
            rotationSpeed = Mathf.Lerp(rotationSpeed, randomRotation, 0.001f);

        }
        else if (changeChance == 1)
        {
            Debug.Log("Stop");
            rotationSpeed = 0;
        }
        else
        {
            Debug.Log("newSpeed");
            float randomRotation = Random.Range(-1f, 1f);
            rotationSpeed = Mathf.Lerp(rotationSpeed, randomRotation, 0.1f);
        }
    }
}
