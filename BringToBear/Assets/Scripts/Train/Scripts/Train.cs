using System.Collections.Generic;
using UnityEngine;

public class Train : MonoBehaviour
{
    [SerializeField, Range(0, 10)]
    private int amountOfCarts = 0;
    [SerializeField, Range(0, 5)]
    public float Margin;

    float trainHeight = 0;

    public GameObject CartPrefab;
    public List<GameObject> Carts = new List<GameObject>();

    private float cartMargin;
    private void Start()
    {
        Bounds _cartBound = CartPrefab.GetComponent<SpriteRenderer>().bounds;
        trainHeight = ((_cartBound.extents.y * 2) * amountOfCarts);

        cartMargin = trainHeight / amountOfCarts;
        PopulateCarts();
    }

    private void Update()
    {
        RePopulate();
    }

    private void PopulateCarts()
    {
        float _startPos = trainHeight;
        Bounds _cartBound = CartPrefab.GetComponent<SpriteRenderer>().bounds;
        _startPos -= _cartBound.extents.y;

        for (int i = 0; i < amountOfCarts; i++)
        {
            Carts.Add(Instantiate(CartPrefab, new Vector3(transform.position.x, transform.position.y - _startPos, 0), Quaternion.identity, transform));

            _startPos -= cartMargin + Margin;
            //blop blip blap
        }
    }

    private void RePopulate()
    {
        if (Carts.Count == amountOfCarts) { return; }

        int _cartsToAdd = amountOfCarts - Carts.Count;

        for (int i = 0; i < _cartsToAdd; i++)
        {




        }
    }

    private void OnDrawGizmosSelected()
    {
        Bounds _cartBound = CartPrefab.GetComponent<SpriteRenderer>().bounds;
        float _gizmoHeight = ((_cartBound.extents.y * 2) * amountOfCarts);
        _gizmoHeight += (Margin * amountOfCarts);
        Gizmos.color = Color.green;

        Gizmos.DrawWireCube(new Vector3(0, transform.position.y - (_gizmoHeight / 2), 0), new Vector3(5, _gizmoHeight, 0));
    }
}
