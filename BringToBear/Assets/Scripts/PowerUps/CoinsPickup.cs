using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinsPickup : MonoBehaviour, IInteractable
{
    public int score;
    float spawnForce;
    GameObject Bullet;
    public PlayerController owner;
    ScoreKeeper scoreKeeper;
    
    void Start()
    {
        spawnForce = Random.Range(5, 15);
        try { scoreKeeper = GameObject.FindGameObjectWithTag("GameController").GetComponent<ScoreKeeper>(); }
        catch (System.Exception) { }
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.AddForce(Random.insideUnitCircle.normalized * spawnForce, ForceMode2D.Impulse);
        Quaternion _rotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
        transform.rotation = _rotation;
    }

    void Update()
    {
        if (transform.position.y < -65)
            Destroy(gameObject);
    }

    public void Interact(PlayerController player)
    {
        if (player == owner)
        {
            return;
        }
        scoreKeeper.AddScore(player, score);
        Destroy(gameObject);
    }
}
