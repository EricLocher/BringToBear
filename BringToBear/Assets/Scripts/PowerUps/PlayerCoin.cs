using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCoin : MonoBehaviour, IInteractable
{
    public int score;
    float spawnForce;
    public PlayerController owner;
    ScoreKeeper scoreKeeper;

    void Start()
    {
        Destroy(gameObject, 5f);
        spawnForce = 50;
        scoreKeeper = GameObject.FindGameObjectWithTag("GameController").GetComponent<ScoreKeeper>();
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.AddForce(-owner.transform.up * spawnForce, ForceMode2D.Impulse);
        Quaternion _rotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
        transform.rotation = _rotation;
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Mouth"))
        {
            Destroy(gameObject);
            scoreKeeper.DepositScore(owner, score);
        }

    }
}
