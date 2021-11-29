using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipAnimation : MonoBehaviour
{
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        float v = Input.GetAxis("Horizontal");
        animator.SetFloat("Rotation", v);
        /*

        if (Input.GetKey(KeyCode.RightShift))
        {
            Debug.Log("Hello");
            animator.Play("FlipShipLeft");
            return;
        }*/
    }
}
