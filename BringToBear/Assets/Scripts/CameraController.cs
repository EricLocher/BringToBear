using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraController : MonoBehaviour
{
    public GameObject map;
    public EdgeCollider2D cameraEdges;

    List<PlayerController> Players;

    public float minRotation;
    public float maxRotation;

    public int minSize, maxSize;
    public float viewMinX, viewMaxX;
    public float viewMinY, viewMaxY;

    Quaternion targetRot;
    Quaternion prevRot;

    private float time;
    float maxTilt = 0;

    public AnimationCurve cameraAcceleration;

    void Start()
    {
        Players = GameController.Players;
        cameraEdges = GetComponentInChildren<EdgeCollider2D>();
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
        maxTilt += Time.deltaTime / 4;
        maxTilt = Mathf.Clamp(maxTilt, 1, 60);

        time += Time.deltaTime * 0.1f;
        time = Mathf.Clamp(time, 0f, 1f);
        transform.rotation = Quaternion.Lerp(prevRot, targetRot, cameraAcceleration.Evaluate(time));
    }

    private void FixedUpdate()
    {
        CameraZoom();
        UpdateEdges();
    }
    public void CameraZoom()
    {

        if (Players.Count <= 0) { return; }
        float minX = GameController.Players[0].transform.position.x,
              maxX = GameController.Players[0].transform.position.x,
              minY = GameController.Players[0].transform.position.y,
              maxY = GameController.Players[0].transform.position.y;

        foreach (PlayerController player in Players)
        {
            if (player.state == PlayerState.Dead) { return; }
            if (player.rb.velocity.magnitude > 120) { continue; }
            if (player == Players[0]) { continue; }
            if (Players.Count < 2) { break; } 

            if (
                Mathf.Abs(transform.position.y - player.transform.position.y) > (maxSize)
                ||
                Mathf.Abs(transform.position.x - player.transform.position.x) > (maxSize) * Camera.main.aspect
               )

            {
                continue;
            }

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

        if(minY < maxY - (45 * 1.8) && GameController.GetGamestate() == GameStates.Playing)
        {
            minY = maxY;
        }
        else if (maxY > minY + (45 * 1.8) && GameController.GetGamestate() == GameStates.MamaBearAproaching)
        {
            maxY = minY;
        }


        Vector2 cameraCenter = new Vector2(((minX + maxX) / 2), ((minY + maxY) / 2));
        float cameraSize;

        if (maxX - minX > maxY - minY)
        {
            cameraSize = (maxX - minX) / 2;
            cameraSize /= Camera.main.aspect;
        }
        else
        {
            cameraSize = (maxY - minY) / 2;
        }

        cameraSize *= 2;
        float zoomLevel = Mathf.Clamp(cameraSize, minSize, maxSize);

        Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, zoomLevel, 2f * Time.deltaTime);

        cameraCenter.y = Mathf.Clamp(cameraCenter.y, viewMinY, viewMaxY);
        cameraCenter.x = Mathf.Clamp(cameraCenter.x, viewMinX, viewMaxX);

        Vector3 newPos = Vector2.Lerp(transform.position, cameraCenter, Time.fixedDeltaTime * 4);
        newPos.z = -10;
        transform.position = newPos;

    }

    public void Shake(float amount)
    {
        Vector3 _shake = Vector3.zero;
        _shake.x = Random.Range(-1, 1) * amount/2;
        _shake.y = Random.Range(-1, 1) * amount/2;

        Quaternion _rotation = Quaternion.Euler(0, 0, transform.rotation.z + Random.Range(-30, 30) * amount);

        transform.position +=  _shake;
        transform.rotation = _rotation;
    }


    void UpdateEdges()
    {
        cameraEdges.transform.localScale = Vector2.one * Camera.main.orthographicSize / maxSize;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector2(viewMinX, viewMaxY), new Vector2(viewMaxX, viewMaxY));
        Gizmos.DrawLine(new Vector2(viewMinX, viewMinY), new Vector2(viewMaxX, viewMinY));
    }
}
