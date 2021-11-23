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
            
            GameObject Bullet = Instantiate(myGun.bullet, transform.position, transform.rotation);
            Bullet.GetComponent<Rigidbody2D>().velocity = Bullet.transform.up * myGun.bulletSpeed;
            Destroy(Bullet, 10f);
            timer = 0;
        }
    }
}

