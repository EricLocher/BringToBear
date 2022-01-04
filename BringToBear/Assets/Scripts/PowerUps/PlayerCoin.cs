using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCoin : MonoBehaviour, IInteractable
{
    public int score;
    float spawnForce;
    public PlayerController owner;
    Rigidbody2D rb;
    ScoreKeeper scoreKeeper;

    void Start()
    {
        if (GameController.gameState == GameStates.GameOver) { return; }

        Destroy(gameObject, Random.Range(4f, 6f));
        spawnForce = 25;
        scoreKeeper = GameObject.FindGameObjectWithTag("GameController").GetComponent<ScoreKeeper>();
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(-owner.transform.up * spawnForce, ForceMode2D.Impulse);
        Quaternion _rotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
        transform.rotation = _rotation;
    }


    public void Interact(PlayerController player)
    {
        if (player == owner || player.state == PlayerState.Dead) { return; }
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

        if (other.gameObject.CompareTag("Mandible"))
        {
            rb.AddForce((new Vector2(Random.Range(-3, 4), 3)) * 15, ForceMode2D.Impulse);
        }

    }
}
