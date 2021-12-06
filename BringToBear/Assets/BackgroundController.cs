using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    Vector2 cameraPos;
    Vector2 bgPos;

    void Start()
    {
    }

    void Update()
    {
        cameraPos = new Vector3(-Camera.main.transform.position.x, -Camera.main.transform.position.y, -10);
        transform.position = Vector3.Lerp(transform.position, cameraPos, 0.5f * Time.deltaTime);
    }
}
