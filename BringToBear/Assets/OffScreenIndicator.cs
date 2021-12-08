using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffScreenIndicator : MonoBehaviour
{
    public GameObject Player;
    Vector2 center;
    SpriteRenderer PlayerRenderer;
    SpriteRenderer[] IndicatorRenderers;


    private void Start()
    {
        Player = GameController.Players[0].gameObject;
        PlayerRenderer = Player.GetComponentInChildren<SpriteRenderer>();
        IndicatorRenderers = gameObject.GetComponentsInChildren<SpriteRenderer>();
    }

    private void Update()
    {


        Debug.Log(Player);
        transform.rotation = Player.transform.rotation;

        if (Player == null)
        {
            Player = GameController.Players[0].gameObject;
            return;
        }

        center = (Vector2)Camera.main.transform.position;

        if (PlayerRenderer.isVisible == false)
        {


            for (int i = 0; i < IndicatorRenderers.Length; i++)
            {
                IndicatorRenderers[i].GetComponent<SpriteRenderer>().enabled = true;
            }


            Vector2 direction = center - new Vector2(Player.transform.position.x, Player.transform.position.y);
            //direction = direction.normalized;

            RaycastHit2D ray = Physics2D.Raycast(center, -direction, 80);




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
