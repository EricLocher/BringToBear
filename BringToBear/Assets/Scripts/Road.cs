using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : MonoBehaviour
{
    public GameObject roadPrefab;
    public GameObject ringPrefab;

    List<GameObject> Roads = new List<GameObject>();
    List<GameObject> Layer1 = new List<GameObject>();
    List<GameObject> Layer2 = new List<GameObject>();
    public List<GameObject> Planets = new List<GameObject>();
    public List<GameObject> Asteroids = new List<GameObject>();

    float roadHeight = 99.5f;
    
    int amountOfRoad = 16;
    int amountOfRings = 1;
    int amountOfPlanets = 4;
    int amountOfAsteroids = 5;


    void Start()
    {
        PopulateRoad();
        StartCoroutine(SpawnPlanet(0.1f));
        StartCoroutine(SpawnAsteroid(0.1f));
    }
    void Update()
    {
        transform.GetChild(0).Translate(Vector2.down * 300 * Time.deltaTime);
        transform.GetChild(1).Translate(Vector2.down * 100 * Time.deltaTime);
        transform.GetChild(2).Translate(Vector2.down * 30 * Time.deltaTime);

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

                road.transform.position = new Vector2(road.transform.position.x, topPos + roadHeight / 2);
            }
        }

        foreach (GameObject planet in Layer2)
        {
            if (planet.transform.position.y < -250)
            {
                Layer2.Remove(planet);
                Destroy(planet);
                break;
            }
        }

        foreach (GameObject asteroid in Layer1)
        {
            if (asteroid.transform.position.y < -250)
            {
                Layer1.Remove(asteroid);
                Destroy(asteroid);
                break;
            }
        }



    }

    void PopulateRoad()
    {
        float _startPos = roadHeight;
     
        for (int i = 0; i < amountOfRoad; i++)
        {
            GameObject _roadToAdd = Instantiate(roadPrefab, new Vector3(transform.position.x, transform.position.y - _startPos, 0), Quaternion.identity, transform.GetChild(0));

            Roads.Add(_roadToAdd);

            _startPos -= roadHeight;
        }

        _startPos = roadHeight;

        for (int i = 0; i < amountOfRings; i++)
        {
            GameObject _ringToAdd = Instantiate(ringPrefab, new Vector3(transform.position.x, transform.position.y - _startPos, 0), Quaternion.identity, transform.GetChild(0));

            Roads.Add(_ringToAdd);

            _startPos -= (roadHeight * amountOfRoad) / amountOfRings;
        }

    }
    IEnumerator SpawnAsteroid(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Quaternion _rotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
        Layer1.Add(Instantiate(Asteroids[Random.Range(0, Asteroids.Count)], new Vector2(Random.Range(-90, 90), 200), _rotation, transform.GetChild(1)));
        StartCoroutine(SpawnAsteroid(0.2f));
    }

    IEnumerator SpawnPlanet(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Quaternion _rotation = Quaternion.Euler(0, 0, Random.Range(-45, 45));
        Layer2.Add(Instantiate(Planets[Random.Range(0, Planets.Count)], new Vector2(Random.Range(-90, 90), 200), _rotation, transform.GetChild(2)));
        StartCoroutine(SpawnPlanet(Random.Range(10f, 25f)));
    }




}
