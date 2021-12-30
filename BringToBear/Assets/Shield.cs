using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    Animator animator;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        animator.SetBool("Hit", true);
    }

    private void LateUpdate()
    {
        animator.SetBool("Hit", false);
    }

}
