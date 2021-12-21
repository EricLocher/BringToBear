using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : MonoBehaviour
{
    public GameObject roadPrefab;
    public GameObject ringPrefab;
    public List<GameObject> Roads = new List<GameObject>();
    public List<GameObject> Rings = new List<GameObject>();

    float roadHeight = 99.5f;
    
    int amountOfRoad = 8;
    int amountOfRings = 1;


    void Start()
    {
        PopulateRoad();
    }

    void PopulateRoad()
    {
        float _startPos = roadHeight;
     
        for (int i = 0; i < amountOfRoad; i++)
        {
            GameObject _roadToAdd = Instantiate(roadPrefab, new Vector3(transform.position.x, transform.position.y - _startPos, 0), Quaternion.identity, transform);

            Roads.Add(_roadToAdd);

            _startPos -= 99.5f;


            //yeah :v
        }

        _startPos = roadHeight;

        for (int i = 0; i < amountOfRings; i++)
        {
            GameObject _ringToAdd = Instantiate(ringPrefab, new Vector3(transform.position.x, transform.position.y - _startPos, 0), Quaternion.identity, transform);

            Roads.Add(_ringToAdd);

            _startPos -= (roadHeight * amountOfRoad) / amountOfRings;


            //yeah :v
        }

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.down * 200 * Time.deltaTime);
        foreach (GameObject road in Roads)
        {
            if (road.transform.position.y < -199)
            {
                float topPos = 0;
                for (int i = 0; i < Roads.Count; i++)
                {
                    if (Roads[i].gameObject == road) { continue; }
                    if (Roads[i] == null) { break; }
                    if (Roads[i].transform.position.y > topPos)
                        topPos = Roads[i].transform.position.y;
                }

                road.transform.position = new Vector2(road.transform.position.x, topPos + 99.5f/2); 
            }
        }

      

    }



}
