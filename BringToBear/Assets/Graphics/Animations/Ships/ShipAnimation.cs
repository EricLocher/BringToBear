using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipAnimation : MonoBehaviour
{
    public Animator animator;
    float rotation = 0;

    private void Update()
    {
        animator.SetFloat("Rotation", rotation);
    }

    public void updateRotation(float value)
    {
        rotation = value;
    }
}