using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleMovement : MonoBehaviour
{
    float xScroll;
    float yScroll;
    void Start()
    {
        yScroll = Random.Range(-1.2f, -3);
        
    }

    void Update()
    {
        xScroll = Random.Range(-0.2f, 0.2f);
        transform.Translate(xScroll, yScroll, 0);
        if (transform.position.y <= -50)
        {
            Destroy(gameObject);
        }
    }
}
