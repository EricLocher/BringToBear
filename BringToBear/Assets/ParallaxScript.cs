using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxScript : MonoBehaviour
{

    public GameObject Particle;
    SpriteRenderer sr;
    float xOffset;
    float yOffset;
    // Start is called before the first frame update
    void Start()
    {
        
    }
        
    // Update is called once per frame
    void Update()
    {
        InvokeRepeating("SpawnParticle", 0, 8);
    }

    public void SpawnParticle()
    {
        xOffset = Random.Range(-17, 17);
        yOffset = Random.Range(30, 120);
        GameObject newParticle = Instantiate(Particle, new Vector3(xOffset, yOffset, 0), Quaternion.identity);
    }
}
