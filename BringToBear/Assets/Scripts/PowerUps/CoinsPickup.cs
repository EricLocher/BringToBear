using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinsPickup : MonoBehaviour, IInteractable
{
    public int score;
    float spawnForce;
    ScoreKeeper scoreKeeper;
    void Start()
    {
        Destroy(gameObject, 15f);
        spawnForce = Random.Range(5, 15);
        scoreKeeper = GameObject.FindGameObjectWithTag("GameController").GetComponent<ScoreKeeper>();
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.AddForce(Random.insideUnitCircle.normalized * spawnForce, ForceMode2D.Impulse);
        Quaternion _rotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
        transform.rotation = _rotation;
    }
    public void Interact(PlayerController player)
    {
        scoreKeeper.AddScore(player, score);
        Destroy(gameObject);
    }
}
