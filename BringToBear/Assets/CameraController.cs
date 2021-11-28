using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    bool clockWise;
    float rotationSpeed = 0.02f;
    bool zoomOut;
    public Transform player0;
    public Transform player1;

    void Start()
    {
        transform.Rotate(0, 0, 0);
        InvokeRepeating("Rotate", 1, 0.01f);
        InvokeRepeating("ChangeDirection", 10, 15f);
    }

    // Update is called once per frame
    void Update()
    {
        
        CameraZoom();
        
    }

    public void Rotate()
    {

        if (clockWise)
        {
            transform.Rotate(0, 0, rotationSpeed);
            //transform.localRotation = new Quaternion(transform.localRotation.x, transform.localRotation.y, Mathf.Clamp(transform.localRotation.z, -0.7f, 0.7f), 0);
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
            float randomRotation = Random.Range(-5f, 5f);
            rotationSpeed = Mathf.Lerp(rotationSpeed, randomRotation, 0.01f);
        }
    }

    public void CameraZoom()
    {
        if (player1 != null)
        {
            float distance = Vector3.Distance(player0.position, player1.position);
            float zoomLevel = Mathf.Clamp(distance, 10, 20);
            Camera.main.orthographicSize = zoomLevel;
        }
        else
        {
            Camera.main.orthographicSize = 18;
        }

    }


}
