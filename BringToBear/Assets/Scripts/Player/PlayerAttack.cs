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

        //TODO: Change input to RIGHT TRIGGER
        if (Input.GetMouseButton(0) && timer > myGun.fireRate)
        {
            GameObject Bullet = Instantiate(myGun.bullet, transform.position, transform.rotation);
            Bullet.GetComponent<Rigidbody2D>().velocity = Bullet.transform.up * 10;
            Destroy(Bullet, 100f);
            timer = 0;
        }
    }
}

