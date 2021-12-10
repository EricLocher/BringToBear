using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinsPickup : MonoBehaviour, IInteractable
{
    public int score;
    ScoreKeeper scoreKeeper;
    
    void Start()
    {
        //scoreKeeper = GameObject.FindGameObjectWithTag("GameController").GetComponent<ScoreKeeper>();
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.AddForce(Random.insideUnitCircle.normalized * 5, ForceMode2D.Impulse);
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
        //scoreKeeper.AddScore(player, score);
        Destroy(gameObject);
    }

}
