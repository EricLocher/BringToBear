using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerDash : MonoBehaviour
{
    [SerializeField, Range(0, 20)] 
    float distance;
    [SerializeField] LayerMask layer;
    [SerializeField] float dashTime;

    public void Dash()
    {

        Vector2 _dir = (transform.up * distance).normalized;
        Rigidbody2D _rb = GetComponent<Rigidbody2D>();
        RaycastHit2D[] _hit = Physics2D.RaycastAll(transform.position, _dir, distance, layer);
        
        if(_hit.Length > 1)
        {
            if (_hit[1].collider != null)
            {

                transform.DOMove(_hit[1].point, dashTime * ((_hit[1].point - (Vector2)transform.position).magnitude / distance));
                StartCoroutine(Dashing(dashTime * ((_hit[1].point - (Vector2)transform.position).magnitude / distance)));
            }
        }
        else
        {
            transform.DOMove(transform.position + transform.up * distance, dashTime);
            StartCoroutine(Dashing(dashTime));
        }

        _rb.AddForce(transform.up * 1000, ForceMode2D.Impulse);

    }


    IEnumerator Dashing(float time)
    {
        yield return new WaitForSeconds(time);
        Rigidbody2D _rb = GetComponent<Rigidbody2D>();
        _rb.velocity = Vector2.zero;
        _rb.AddForce(transform.up * 200, ForceMode2D.Impulse);
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawLine((Vector2)transform.position, (Vector2)transform.position + (Vector2)(transform.up * distance));
    }

}
