using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipAnimation : MonoBehaviour
{
    Animator animator;
    float rotation = 0;

    void Start()
    {
        animator = GetComponent<Animator>();

    }

    private void Update()
    {
        //float v = Input.GetAxis("Horizontal");
        animator.SetFloat("Rotation", rotation);
    }

    public void updateRotation(float value)
    {
        rotation = value;
    }
}