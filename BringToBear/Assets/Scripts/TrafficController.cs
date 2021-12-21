using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficController : MonoBehaviour
{
    public GameObject TrafficVehicle1;
    public GameObject TrafficVehicle2;
    public GameObject TrafficVehicle3;

    float xOffset;
    float yOffset;
    float minYSpawn = 100;
    float maxYSpawn = 300;

    public float traffic1Amount;
    public float traffic2Amount;
    public float traffic3Amount;
    public static TrafficController INSTANCE;
    public SpriteRenderer spriteRenderer;

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

        for (int i = 0; i < traffic1Amount; i++)
        {
            Vector3 newPos = GetFreeVehiclePosition(0);
            GameObject newTrafficVehicle = Instantiate(TrafficVehicle1, newPos, Quaternion.identity);
            newTrafficVehicle.GetComponent<TrafficBehaviour>().spriteRenderer = spriteRenderer;
            Vehicles.Add(newTrafficVehicle.transform);

        }
        for (int i = 0; i < traffic2Amount; i++)
        {
            Vector3 newPos = GetFreeVehiclePosition(0);
            GameObject newTrafficVehicle2 = Instantiate(TrafficVehicle2, newPos, Quaternion.identity);
            newTrafficVehicle2.GetComponent<TrafficBehaviour>().spriteRenderer = spriteRenderer;
            Vehicles.Add(newTrafficVehicle2.transform);
        }
        for (int i = 0; i < traffic2Amount; i++)
        {
            Vector3 newPos = GetFreeVehiclePosition(0);
            GameObject newTrafficVehicle3 = Instantiate(TrafficVehicle3, newPos, Quaternion.identity);
            newTrafficVehicle3.GetComponent<TrafficBehaviour>().spriteRenderer = spriteRenderer;
            Vehicles.Add(newTrafficVehicle3.transform);
        }
    }

    public Vector3 GetFreeVehiclePosition(int escape)
    {


        xOffset = Random.Range(spriteRenderer.bounds.min.x, spriteRenderer.bounds.max.x);
        yOffset = Random.Range(minYSpawn, maxYSpawn);
        Vector3 pos = new Vector3(xOffset, yOffset);

        bool allowed = true;

        if (escape > 200)
        {
            Debug.Log("forced escape");
            return pos;
        }

        foreach (Transform item in Vehicles)
        {
            if (Vector3.Distance(item.position, pos) < 10)
            {
                allowed = false;
                break;
            }
        }

        return allowed ? pos : GetFreeVehiclePosition(escape + 1);
    }
}
