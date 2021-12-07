using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Gun myGun;
    float timer = 0;

    void Update()
    {
        timer += Time.deltaTime;
    }

    public void Attack()
    {
        if (timer > myGun.fireRate)
        {
            float _spacing = myGun.spread / (float)myGun.amountOfGuns;

            Vector3 _Pos = new Vector3(-_spacing * ((float)myGun.amountOfGuns / 2) + _spacing / 2, 0, 0);

            float playerRotation = transform.eulerAngles.z;
            float randomRotation;

            for (int i = 0; i < myGun.amountOfGuns; i++)
            {
                GameObject _Bullet = Instantiate(myGun.bullet, transform.position + _Pos, transform.rotation);
                _Bullet.transform.position = transform.rotation * (_Bullet.transform.position - transform.position) + transform.position;

                if (myGun.spreadMode)
                {
                    randomRotation = playerRotation + Random.Range(-myGun.spreadRotation, myGun.spreadRotation);
                    _Bullet.transform.rotation = Quaternion.Euler(0, 0, randomRotation);
                }

                _Bullet.GetComponent<IBullet>().SetOwner(transform.gameObject);
                Destroy(_Bullet, 10f);

                _Pos.x += _spacing;
            }

            timer = 0;
        }
    }
}

