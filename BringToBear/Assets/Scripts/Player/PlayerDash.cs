using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    [SerializeField, Range(0, 20)] 
    float distance;

    public void Dash()
    {
        transform.position = (Vector2)transform.position * distance;
        Debug.Log(transform.position + transform.forward * distance);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * distance);
    }

}
