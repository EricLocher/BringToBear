using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KOCoin : MonoBehaviour, IInteractable
{
    public int score;
    float spawnForce;
    float timer;
    ScoreKeeper scoreKeeper;


    void Start()
    {
        Destroy(gameObject, 5f);
        spawnForce = Random.Range(2.4f, 3f);
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        Vector2 direction = Camera.main.transform.position - transform.position;
        rb.AddForce(direction * spawnForce, ForceMode2D.Impulse);
        rb.gravityScale = Random.Range(2f, 4f);
        rb.drag = Random.Range(2f, 4f);
        Quaternion _rotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
        transform.rotation = _rotation;
        scoreKeeper = GameObject.FindGameObjectWithTag("GameController").GetComponent<ScoreKeeper>();
    }

    void Update()
    {
        timer += Time.deltaTime;
    }

    public void Interact(PlayerController player)
    {
        if (timer > 0.5f)
        {
            scoreKeeper.AddScore(player, score);
            Destroy(gameObject);
        }
    }
}
