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
    IEnumerator currentCoroutine;

    public bool dashing;
    public void Dash()
    {
        Rigidbody2D _rb = GetComponent<Rigidbody2D>();

        if(Mathf.Abs(_rb.velocity.y) > 100 || Mathf.Abs(_rb.velocity.x) > 100) { return; }

        transform.DOKill();
        if(currentCoroutine != null)
        StopCoroutine(currentCoroutine);

        Vector2 _dir = (transform.up * distance).normalized;
        RaycastHit2D[] _hit = Physics2D.RaycastAll(transform.position, _dir, distance, layer);


        //dashing = true;

        if(_hit.Length > 1)
        {
            if (_hit[1].collider != null)
            {

                transform.DOMove(_hit[1].point, dashTime * ((_hit[1].point - (Vector2)transform.position).magnitude / distance));
                currentCoroutine = Dashing(dashTime * ((_hit[1].point - (Vector2)transform.position).magnitude / distance));
            }
        }
        else
        {
            transform.DOMove(transform.position + transform.up * distance, dashTime);
            currentCoroutine = Dashing(dashTime);
        }

        StartCoroutine(currentCoroutine);
        _rb.AddForce(transform.up * 1000, ForceMode2D.Impulse);

    }

    IEnumerator Dashing(float time)
    {
        yield return new WaitForSeconds(time);
        Rigidbody2D _rb = GetComponent<Rigidbody2D>();
        _rb.velocity = Vector2.zero;
        _rb.AddForce(transform.up * 100, ForceMode2D.Impulse);
        dashing = false;
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawLine((Vector2)transform.position, (Vector2)transform.position + (Vector2)(transform.up * distance));
    }

}
