using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    Animator animator;
    public PlayerController player;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        animator.SetBool("Hit", true);

        if (other.CompareTag("Player") || other.CompareTag("Shield"))
        {

            PlayerController _playerController = other.GetComponent<PlayerController>();

            if (other.CompareTag("Shield"))
            {
                _playerController = other.GetComponentInParent<PlayerController>();
            }
            Rigidbody2D _otherRb = _playerController.GetComponent<Rigidbody2D>();

            if (!player.invincible && !_playerController.invincible)
            {
                Debug.Log("WHA");
                CollisionHandler.DoCollision(player.rb, _otherRb, 2, other.ClosestPoint(transform.position));
                player.invincible = true;
                StartCoroutine(player.InvinceTime());
            }
        }

    }

    private void LateUpdate()
    {
        animator.SetBool("Hit", false);
    }

}
