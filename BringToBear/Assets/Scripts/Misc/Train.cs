using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Train : MonoBehaviour
{
    [SerializeField, Range(0, 10)]
    private int amountOfCarts = 0;
    [SerializeField, Range(-5, 5)]
    public float Margin;

    float trainHeight = 0;
    Bounds cartBound;
    public GameObject CartPrefab;
    public List<GameObject> Carts = new List<GameObject>();

    float seed;

    private float cartMargin;
    private void Start()
    {

        seed = Random.Range(0, 100000);
        cartBound = CartPrefab.GetComponent<SpriteRenderer>().bounds;
        trainHeight = ((cartBound.extents.y * 2) * amountOfCarts);

        cartMargin = trainHeight / amountOfCarts;
        PopulateCarts();
    }

    private void Update()
    {
        RePopulate();
        MoveCarts();
        RandomMove();
    }
    public void DestroyedCart(GameObject cart)
    {

        int _cartIndex = Carts.FindIndex(a => a.gameObject == cart);
        if(_cartIndex == -1) { Debug.LogError("Something went wrong..."); return; }

        Carts.Remove(Carts[_cartIndex]);
        for (int i = _cartIndex - 1; i >= 0; i--)
        {
            Rigidbody2D _rb = Carts[i].GetComponent<Rigidbody2D>();
            _rb.gravityScale = 3;
            _rb.angularVelocity = Random.Range(-30, 30);
            Destroy(Carts[i], 10f);
            Carts.Remove(Carts[i]);
        }

    }

    private void PopulateCarts()
    {
        float _startPos = trainHeight;
        cartBound = CartPrefab.GetComponent<SpriteRenderer>().bounds;
        _startPos -= cartBound.extents.y;

        for (int i = 0; i < amountOfCarts; i++)
        {
            GameObject _cartToAdd = Instantiate(CartPrefab, new Vector3(transform.position.x, transform.position.y - _startPos, 0), Quaternion.identity, transform);

            Carts.Add(_cartToAdd);

            _startPos -= cartMargin + Margin;


            //blop blip blap
        }

        for (int i = Carts.Count - 1; i >= 0; i--)
        {
            if (i == Carts.Count - 1) { continue; }
            Carts[i].GetComponent<Cart>().connectedCart = Carts[i + 1].GetComponent<Cart>();

        }
    }

    private void RePopulate()
    {
        if (Carts.Count >= amountOfCarts) { return; }

        int _cartsToAdd = amountOfCarts - Carts.Count;

        for (int i = 1; i < _cartsToAdd + 1; i++)
        {
            Vector2 _topPos = Carts[Carts.Count - 1].transform.position;

            GameObject _cartToAdd = Instantiate(CartPrefab, new Vector3(_topPos.x, _topPos.y + (cartBound.extents.y * 2) + Margin, 0), Quaternion.identity, transform);
            Carts[Carts.Count - 1].GetComponent<Cart>().connectedCart = _cartToAdd.GetComponent<Cart>();
            Carts.Add(_cartToAdd);
            _topPos.y += cartBound.extents.y * 2;
        }
    }

    private void MoveCarts()
    {
        if (Carts[0].transform.localPosition.y - cartBound.extents.y > -(trainHeight + Margin) + 0.001)
        {
            foreach (GameObject cart in Carts)
            {
                //cart.transform.position -= new Vector3(0, 10f * Time.deltaTime, 0);
                cart.transform.DOMove(cart.transform.position - new Vector3(0, 100, 0), 10f);
            }
        }
        else
        {
            foreach (GameObject cart in Carts)
            {
                cart.transform.DOKill();
            }
        }
    }

    private void RandomMove()
    {
        float _moveX = Mathf.PerlinNoise(seed, Time.time) - 0.5f;

        _moveX /= 10;

        _moveX += ((Carts[Carts.Count - 1].transform.position.x - transform.position.x) * -1) * Time.deltaTime;

        _moveX *= 2;

        Vector2 _newPos = Vector2.zero;

        _newPos.x = Carts[Carts.Count - 1].transform.position.x + _moveX;
        _newPos.y = Carts[Carts.Count - 1].transform.position.y;

        Carts[Carts.Count - 1].transform.position = _newPos;
    }

    private void OnDrawGizmosSelected()
    {
        Bounds _cartBound = CartPrefab.GetComponent<SpriteRenderer>().bounds;
        float _gizmoHeight = ((_cartBound.extents.y * 2) * amountOfCarts);
        _gizmoHeight += (Margin * amountOfCarts);
        Gizmos.color = Color.green;

        Gizmos.DrawWireCube(new Vector3(transform.position.x, transform.position.y - (_gizmoHeight / 2), 0), new Vector3(5, _gizmoHeight, 0));
    }
}
