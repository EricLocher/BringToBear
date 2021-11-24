using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficController : MonoBehaviour
{
    public GameObject TrafficVehicle;
    public GameObject Particle;
    float xOffset;
    float yOffset;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 10; i++)
        {
            xOffset = Random.Range(-17, 17);
            yOffset = Random.Range(30, 120);
            GameObject newTrafficVehicle = Instantiate(TrafficVehicle, new Vector3(xOffset, yOffset, 0), Quaternion.identity);
        }

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Traffic"))
        {
            Destroy(other.gameObject);
            xOffset = Random.Range(-17, 17);
            yOffset = Random.Range(30, 70);
            GameObject newTrafficVehicle = Instantiate(TrafficVehicle, new Vector3(xOffset, yOffset, 0), Quaternion.identity);
        }

    }


}
