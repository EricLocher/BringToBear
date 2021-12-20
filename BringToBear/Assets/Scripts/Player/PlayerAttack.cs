using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Gun defaultGun;
    public Gun myGun;
    public GameObject muzzleFlash;
    public int myAmmo = 0;
    float timer = 0;
    public AudioSource audioSource0;
    public AudioSource audioSource1;


    private void Start()
    {
   
    }

    void Update()
    {
        timer += Time.deltaTime;
    }

    public void Attack()
    {
        if (myAmmo <= 0 && !myGun.infAmmo)
        {
            myGun = defaultGun;
        }

        if (timer > myGun.fireRate)
        {
            float _spacing = myGun.spread / (float)myGun.amountOfGuns;

            Vector3 _Pos = new Vector3(-_spacing * ((float)myGun.amountOfGuns / 2) + _spacing / 2, 0, 0);

            float playerRotation = transform.eulerAngles.z;
            float randomRotation;


            try
            {
                audioSource0.PlayOneShot(myGun.gunSounds0[Random.Range(0, myGun.gunSounds0.Length)], 0.2f);
                audioSource1.PlayOneShot(myGun.gunSounds1[Random.Range(0, myGun.gunSounds1.Length)], 0.1f);
                muzzleFlash.SetActive(true);

            }
            catch (System.Exception)
            {
                
            }



            for (int i = 0; i < myGun.amountOfGuns; i++)
            {
                GameObject _Bullet = Instantiate(myGun.bullet, transform.position + _Pos, transform.rotation);
                _Bullet.transform.position = transform.rotation * (_Bullet.transform.position - transform.position) + transform.position;



                if (myGun.spreadMode)
                {
                    randomRotation = playerRotation + Random.Range(-myGun.spreadRotation, myGun.spreadRotation);
                    _Bullet.transform.rotation = Quaternion.Euler(0, 0, randomRotation);
                }

                _Bullet.GetComponent<IBullet>().Owner = gameObject;

                Destroy(_Bullet, 10f);

                _Pos.x += _spacing;
                myAmmo--;
            }
            timer = 0;
        }
    }

    public void SetWeapon(Gun weapon)
    {
        myGun = weapon;
        myAmmo = weapon.ammo;
    }

}
