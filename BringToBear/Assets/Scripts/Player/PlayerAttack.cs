using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Gun myGun;
    float timer = 0;

    void Update()
    {
        timer += Time.deltaTime;

        if (Input.GetAxis("R2") > 0 && timer > myGun.fireRate)
        {

            float _spacing = 2/(float)myGun.amountOfGuns;
            Vector3 _Pos = new Vector3(-_spacing * ((float)myGun.amountOfGuns / 2) + _spacing/2, 0, 0);
            for (int i = 0; i < myGun.amountOfGuns; i++)
            {

                GameObject _Bullet = Instantiate(myGun.bullet, transform.position + _Pos, transform.rotation);
                Quaternion rot = transform.rotation;
                _Bullet.transform.position = rot * (_Bullet.transform.position - transform.position) + transform.position;
                _Bullet.GetComponent<Rigidbody2D>().velocity = _Bullet.transform.up * myGun.bulletSpeed;
                Destroy(_Bullet, 100f);
                timer = 0;

                _Pos.x += _spacing;
            }

            timer = 0;
        }
    }
}

