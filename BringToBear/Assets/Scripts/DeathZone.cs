using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    [SerializeField] float height;
    [SerializeField] float width;
    public GameObject KO;

    private void Update()
    {
        foreach (PlayerController player in GameController.Players)
        {
            if (player.state == PlayerState.Dead) { return; }

            if (
                Mathf.Abs((player.transform.position.x - transform.position.x)) > width / 2
                ||
                Mathf.Abs((player.transform.position.y - transform.position.y)) > height / 2
              )
            {


                foreach (OffScreenIndicator indicator in GameController.Indicators)
                {
                    Debug.Log(player.gameObject);
                    Debug.Log(indicator.Player.gameObject);


                    if (player.gameObject == indicator.Player.gameObject)
                    {
                        GameObject _tempKO = Instantiate(KO, indicator.transform.position, Quaternion.identity, transform);
                        Vector2 _dis = transform.position - _tempKO.transform.position;
                        float _angle = Mathf.Atan2(_dis.y, _dis.x) * Mathf.Rad2Deg;
                        _tempKO.transform.rotation = Quaternion.Euler(new Vector3(0, 0, _angle));
                    }
                }
                player.Res();
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireCube(transform.position, new Vector3(width, height, 0));

    }
}
