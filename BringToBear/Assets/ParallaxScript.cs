using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxScript : MonoBehaviour
{

    public GameObject Particle0;
    public GameObject Particle1;
    public GameObject Particle2;
    float xOffset;
    float yOffset;

    SpriteRenderer spriteRenderer;

    
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        InvokeRepeating("SpawnParticle", 0.1f, 0.001f);
    }
        

    void Update()
    {
        
    }

    public void SpawnParticle()
    {
        xOffset = Random.Range(-17, 17);
        yOffset = Random.Range(30, 120);

        GameObject newParticle0 = Instantiate(Particle0, new Vector3(xOffset, yOffset, 0), Quaternion.identity);
        GameObject newParticle1 = Instantiate(Particle1, new Vector3(xOffset, yOffset, 0), Quaternion.identity);
        GameObject newParticle2 = Instantiate(Particle2, new Vector3(xOffset, yOffset, 0), Quaternion.identity);

    }
}
