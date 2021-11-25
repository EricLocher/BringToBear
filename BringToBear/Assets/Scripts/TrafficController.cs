using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficController : MonoBehaviour
{
    public GameObject TrafficVehicle;
    float xOffset;
    float yOffset;

    public static TrafficController INSTANCE;

    List<Transform> Vehicles;


    private void Awake()
    {
        if (INSTANCE == null) INSTANCE = this;
        else Destroy(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        Vehicles = new List<Transform>();

        for (int i = 0; i < 10; i++)
        {
            Vector3 newPos = GetFreeVehiclePosition(0);
            GameObject newTrafficVehicle = Instantiate(TrafficVehicle, newPos, Quaternion.identity);

            Vehicles.Add(newTrafficVehicle.transform);

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
          
         
        }

    }

    public Vector3 GetFreeVehiclePosition(int escape)
    {


        xOffset = Random.Range(-17, 17);
        yOffset = Random.Range(30, 120);
        Vector3 pos = new Vector3(xOffset, yOffset);

        bool allowed = true;

        if (escape > 200)
        {
            Debug.Log("forced escape");
            return pos;
        }

        foreach (Transform item in Vehicles)
        {
            if (Vector3.Distance(item.position, pos) < 7)
            {
                allowed = false;
                break;
            }
        }



        return allowed ? pos : GetFreeVehiclePosition(escape + 1);
    }
}
