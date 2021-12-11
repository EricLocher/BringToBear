using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffScreenIndicator : MonoBehaviour
{
    public GameObject Player;
    Vector2 center;
    SpriteRenderer PlayerRenderer;
    SpriteRenderer[] IndicatorRenderers;
    [SerializeField] LayerMask layer;

    private void Start()
    {
        PlayerRenderer = Player.GetComponentInChildren<SpriteRenderer>();
        IndicatorRenderers = GetComponentsInChildren<SpriteRenderer>();
    }

    private void Update()
    {


        transform.rotation = Player.transform.rotation;

        center = (Vector2)Camera.main.transform.position;
        Vector2 playerPosRelCam = Camera.main.WorldToViewportPoint(Player.transform.position);
        if (playerPosRelCam.x < 0 || playerPosRelCam.x > 1 || playerPosRelCam.y < 0 || playerPosRelCam.y > 1)
        {


            for (int i = 0; i < IndicatorRenderers.Length; i++)
            {
                IndicatorRenderers[i].GetComponent<SpriteRenderer>().enabled = true;
            }


            Vector2 direction = center - new Vector2(Player.transform.position.x, Player.transform.position.y);
            Debug.DrawRay(center, -direction, Color.red);
            direction = direction.normalized;

            RaycastHit2D ray = Physics2D.Raycast(center, -direction, 80, layer);


            if (ray.collider != null)
            {
                transform.position = ray.point;
            }





        }
        else
        {
            for (int i = 0; i < IndicatorRenderers.Length; i++)
            {
                IndicatorRenderers[i].GetComponent<SpriteRenderer>().enabled = false;
            }
        }

    }

}
