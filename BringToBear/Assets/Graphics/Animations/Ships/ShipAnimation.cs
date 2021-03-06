using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipAnimation : MonoBehaviour
{
    public Animator animator;
    public Animator outline;
    public SpriteRenderer playerOutline;
    float rotation = 0;
    

    private void Update()
    {
        animator.SetFloat("Rotation", rotation);
        outline.SetFloat("Rotation", rotation);
    }

    public void updateRotation(float value)
    {
        rotation = value;
    }

    public void Hit()
    {
        animator.SetBool("Hit", true);
    }

    private void LateUpdate()
    {
        animator.SetBool("Hit", false);
    }
}