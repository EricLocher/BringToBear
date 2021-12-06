using System.Collections.Generic;
using UnityEngine;

public class Train : MonoBehaviour
{
    [SerializeField, Range(0, 10)]
    private int amountOfCarts = 0;
    [SerializeField, Range(0, 5)]
    public float Margin;

    float trainHeight = 0;
    Bounds cartBound;
    public GameObject CartPrefab;
    public List<GameObject> Carts = new List<GameObject>();

    private float cartMargin;
    private void Start()
    {
        cartBound = CartPrefab.GetComponent<SpriteRenderer>().bounds;
        trainHeight = ((cartBound.extents.y * 2) * amountOfCarts);

        cartMargin = trainHeight / amountOfCarts;
        PopulateCarts();
    }

    private void Update()
    {
        RePopulate();
        MoveCarts();
    }
    public void DestroyedCart(GameObject cart)
    {
        for (int i = 0; i < Carts.Count; i++)
        {
            if(Carts[i] == cart)
            {
                Carts.Remove(Carts[i]);
                for (int j = i; j > 0; j--)
                {
                    Carts.Remove(Carts[j]);
                }
            }
        }
    }

    private void PopulateCarts()
    {
        float _startPos = trainHeight;
        cartBound = CartPrefab.GetComponent<SpriteRenderer>().bounds;
        _startPos -= cartBound.extents.y;

        for (int i = 0; i < amountOfCarts; i++)
        {
            Carts.Add(Instantiate(CartPrefab, new Vector3(transform.position.x, transform.position.y - _startPos, 0), Quaternion.identity, transform));

            _startPos -= cartMargin + Margin;
            //blop blip blap
        }
        //for (int i = 0; i < Carts.Count; i++)
        //{
        //    Debug.Log(i);
        //    if (i == Carts.Count - 1) { continue; }
        //    else
        //    {
        //        Carts[i].GetComponent<Cart>().connectedCart = Carts[i + 1].GetComponent<Rigidbody2D>();
        //        //Carts[i].GetComponent<HingeJoint2D>().connectedBody = Carts[i + 1].GetComponent<Rigidbody2D>();
        //    }
        //}
    }

    private void RePopulate()
    {
        if (Carts.Count >= amountOfCarts) { return; }

        int _cartsToAdd = amountOfCarts - Carts.Count;

        for (int i = 1; i < _cartsToAdd + 1; i++)
        {
            Vector2 _topPos = Carts[Carts.Count - 1].transform.position;
            Debug.Log(cartBound.extents.y * 2);
            Carts.Add(Instantiate(CartPrefab, new Vector3(_topPos.x, _topPos.y + (cartBound.extents.y * 2) + Margin, 0), Quaternion.identity, transform));
            _topPos.y += cartBound.extents.y * 2;
        }
    }
    private void MoveCarts()
    {
        if (Carts[0].transform.localPosition.y - cartBound.extents.y > -(trainHeight + Margin) + 0.001)
        {
            foreach (GameObject cart in Carts)
            {
                cart.transform.position -= new Vector3(0, 0.1f, 0);
            }
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
