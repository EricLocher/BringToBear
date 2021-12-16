using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : MonoBehaviour
{
    public GameObject roadPrefab;
    public List<GameObject> Roads = new List<GameObject>();
    
    float roadHeight = 99.5f;
    
    int amountOfRoad = 5;


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
    }

    // Update is called once per frame
    void Update()
    {
            transform.Translate(Vector2.down * 250 * Time.deltaTime);
        foreach (GameObject road in Roads)
        {
            if (road.transform.position.y < -199)
            transform.position = new Vector2(0, 0);
        }
        
            

    }



}
