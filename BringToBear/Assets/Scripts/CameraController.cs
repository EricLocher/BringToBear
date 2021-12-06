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



        time += Time.deltaTime * 0.1f;
        time = Mathf.Clamp(time, 0f, 1f);
        transform.rotation = Quaternion.Lerp(prevRot, targetRot, cameraAcceleration.Evaluate(time));
    }

    public void CameraZoom()
    {
        float minX = Players[0].transform.position.x, 
              maxX = Players[0].transform.position.x, 
              minY = Players[0].transform.position.y,
              maxY = Players[0].transform.position.y;

        foreach (PlayerController player in Players)
        {
            if(player == Players[0]) { continue; }
            Vector2 playerPos = player.transform.position;

            if (playerPos.x > maxX)
                maxX = playerPos.x;

            else if (playerPos.x < minX)
                minX = playerPos.x;

            if (playerPos.y > maxY)
                maxY = playerPos.y;

            else if (playerPos.y < minY)
                minY = playerPos.y;
        }

        Vector2 cameraCenter = new Vector2(((minX + maxX) / 2), ((minY + maxY) / 2));
        float cameraSize;
        if (maxX - minX > maxY - minY)
        {
            cameraSize = (maxX - minX) / 2;
            cameraSize /= Camera.main.aspect;
        }
        else
            cameraSize = (maxY - minY) / 2;

        cameraSize *= 2;
        float zoomLevel = Mathf.Clamp(cameraSize, 18, 45);
        float prevZoom = Camera.main.orthographicSize;
        Camera.main.orthographicSize = Mathf.Lerp(prevZoom, zoomLevel, 2f * Time.deltaTime);

        cameraCenter.y = Mathf.Clamp(cameraCenter.y, -60.5f, 75f);
        cameraCenter.x = Mathf.Clamp(cameraCenter.x, -30f, 30f);
        transform.position = new Vector3(cameraCenter.x, cameraCenter.y, -10);


        //if (Players.Count > 1)
        //{
        //    float distance = Vector3.Distance(Players[0].transform.position, Players[1].transform.position);
        //    float zoomLevel = Mathf.Clamp(distance, 18, 45);
        //    float prevZoom = Camera.main.orthographicSize;
        //    Camera.main.orthographicSize = Mathf.Lerp(prevZoom, zoomLevel, 0.01f);

        //    center = ((Players[0].transform.position + Players[1].transform.position) / 2);
        //    center.x = Mathf.Clamp(center.x, -12f, 12f);
        //}
        //else if(Players.Count != 0)
        //{
        //    center = Players[0].transform.position;
        //    Camera.main.orthographicSize = 18;
        //    center.x = Mathf.Clamp(center.x, -30f, 30f);
        //}

        //center.y = Mathf.Clamp(center.y, -60.5f, 75f);
        //transform.position = new Vector3(center.x, center.y, -10);

    }


}
