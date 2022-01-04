using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinsPickup : MonoBehaviour, IInteractable
{
    public int score;
    float spawnForce;
    ScoreKeeper scoreKeeper;
    public PlayerController owner;
    float timer = 0;
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

    void Update()
    {
        timer += Time.deltaTime;
    }

    public void Interact(PlayerController player)
    {
        if (timer < 2 && player == owner || player.state == PlayerState.Dead) { return; }
        scoreKeeper.AddScore(player, score);
        Destroy(gameObject);
    }
}
