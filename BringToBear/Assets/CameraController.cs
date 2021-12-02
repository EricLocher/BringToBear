using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public GameController GameController;
    List<PlayerController> Players;

    public float minRotation;
    public float maxRotation;


    Quaternion targetRot;
    Quaternion prevRot;

    private float time;
    float maxTilt = 0;
    Vector3 center;

    public AnimationCurve cameraAcceleration;

    void Start()
    {

        Players = GameController.Players;

        transform.Rotate(0, 0, 0);
        targetRot = Quaternion.identity;
        prevRot = targetRot;
        Camera.main.orthographicSize = 18;
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
        CameraZoom();

        maxTilt += Time.deltaTime / 4;
        maxTilt = Mathf.Clamp(maxTilt, 1, 60);


        //Debug.Log(maxTilt);

        time += Time.deltaTime * 0.1f;
        time = Mathf.Clamp(time, 0f, 1f);
        transform.rotation = Quaternion.Lerp(prevRot, targetRot, cameraAcceleration.Evaluate(time));
    }

    public void CameraZoom()
    {
        

        if (Players.Count > 1)
        {
            float distance = Vector3.Distance(Players[0].transform.position, Players[1].transform.position);
            float zoomLevel = Mathf.Clamp(distance, 10, 20);
            float prevZoom = Camera.main.orthographicSize;
            Camera.main.orthographicSize = Mathf.Lerp(prevZoom, zoomLevel, 0.01f);

            center = ((Players[0].transform.position + Players[1].transform.position) / 2);
        }
        else
        {

            center = Players[0].transform.position;
            Camera.main.orthographicSize = 18;
        }
        center.y = Mathf.Clamp(center.y, -60.5f, 75f);
        transform.position = new Vector3(center.x, center.y, -10);

    }


}
