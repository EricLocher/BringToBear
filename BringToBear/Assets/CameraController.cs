using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public Transform player0;
    public Transform player1;
    public float minRotation;
    public float maxRotation;


    Quaternion targetRot;
    Quaternion prevRot;

    bool zoomOut;
    private float time;
    float maxTilt = 0;

    public AnimationCurve cameraAcceleration;

    void Start()
    {
        transform.Rotate(0, 0, 0);
        targetRot = Quaternion.identity;
        prevRot = targetRot;
        Invoke("SetNewRotationTarget", 3);
        
    }



    private void SetNewRotationTarget()
    {
        prevRot = targetRot;
        targetRot = Quaternion.Euler(0, 0, Random.Range(-maxTilt, maxTilt));
        time = 0;
        Invoke("SetNewRotationTarget", Random.Range(10, 20));
    }

    void Update()
    {
        maxTilt += Time.deltaTime / 4;
        maxTilt = Mathf.Clamp(maxTilt, 1, 60);
        

        //Debug.Log(maxTilt);

        time += Time.deltaTime * 0.1f;
        time = Mathf.Clamp(time, 0f, 1f);
        transform.rotation = Quaternion.Lerp(prevRot, targetRot, cameraAcceleration.Evaluate(time));
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
