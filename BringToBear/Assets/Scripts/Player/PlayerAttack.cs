using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Gun defaultGun;
    public Gun myGun;
    [SerializeField] GameObject muzzleFlash;
    [SerializeField] GameObject railGunCharge;
    [SerializeField] GameObject railGunBeam;
    [SerializeField, Range(1, 100)]
    float railGunDistance = 30;
    [SerializeField] int railGunDamage = 1000;
    public int myAmmo = 0;
    public AudioSource audioSource0;
    public AudioSource audioSource1;
    bool chargeUp = false;
    float timer = 0;
    float weaponVolume = 0.2f;





    void Update()
    {
        weaponVolume = myGun.weaponVolume;
        if (myGun.name != "Railgun")
            timer += Time.deltaTime;
        else if (!chargeUp && timer != 0)
        {
            if (timer <= 0)
            {
                railGunCharge.SetActive(false);
                timer = 0;
            }
            else
            {
                timer -= Time.deltaTime * 2;
                railGunCharge.SetActive(true);
                railGunCharge.GetComponent<Animator>().Update(-Time.deltaTime * 2);
            }
        }
        else if (chargeUp)
        {
            timer += Time.deltaTime;
        }
    }

    public void Attack()
    {
        if (myAmmo <= 0 && !myGun.infAmmo)
        {
            myGun = defaultGun;
        }

        if (timer > myGun.fireRate && myGun.name != "Railgun")
        {
            float _spacing = myGun.spread / (float)myGun.amountOfGuns;

            Vector3 _pos = new Vector3(-_spacing * ((float)myGun.amountOfGuns / 2) + _spacing / 2, 0, 0);

            float playerRotation = transform.eulerAngles.z;
            float randomRotation;

            if (myGun == defaultGun)
            {
                muzzleFlash.SetActive(true);
            }

            try
            {
                audioSource0.PlayOneShot(myGun.gunSounds0[Random.Range(0, myGun.gunSounds0.Length)], weaponVolume);
                audioSource1.PlayOneShot(myGun.gunSounds1[Random.Range(0, myGun.gunSounds1.Length)], weaponVolume / 2);
                

            }
            catch (System.Exception)
            {

            }



            for (int i = 0; i < myGun.amountOfGuns; i++)
            {
                GameObject _bullet = Instantiate(myGun.bullet, transform.position + _pos, transform.rotation);
                _bullet.transform.position = transform.rotation * (_bullet.transform.position - transform.position) + transform.position;

                if (myGun.spreadMode)
                {
                    randomRotation = playerRotation + Random.Range(-myGun.spreadRotation, myGun.spreadRotation);
                    _bullet.transform.rotation = Quaternion.Euler(0, 0, randomRotation);
                }

                _bullet.GetComponent<IBullet>().Owner = gameObject;

                Destroy(_bullet, 10f);

                _pos.x += _spacing;
                myAmmo--;




            }

            timer = 0;
        }

        else if (myGun.name == "Railgun")
        {
            StopAllCoroutines();
            StartCoroutine(ChargeUp());
            chargeUp = true;


            if (timer < 0.5f)
            {
                railGunCharge.SetActive(true);
                railGunCharge.GetComponent<Animator>().Update(Time.deltaTime);
                if (!audioSource1.isPlaying)
                    audioSource1.PlayOneShot(myGun.gunSounds1[0], weaponVolume);
            }
            else
            {
                Vector2 _dir = (transform.up * railGunDistance).normalized;
                RaycastHit2D[] _hit = Physics2D.RaycastAll(transform.position, _dir, railGunDistance);

                if (_hit.Length > 0)
                {
                    for (int i = 0; i < _hit.Length; i++)
                    {
                        if (_hit[i].collider != null) { break; }

                        GameObject other = _hit[i].collider.gameObject;

                        if (other != gameObject && other.GetComponent<ICharacter>() != null)
                        {
                            other.GetComponent<ICharacter>().Damage(railGunDamage);
                            Instantiate(myGun.bullet.GetComponent<Bullet>().Explosion, new Vector3(_hit[i].point.x, _hit[i].point.y), Quaternion.identity);
                        }
                    }

                    audioSource0.PlayOneShot(myGun.gunSounds0[0], weaponVolume);
                    GameObject _beam = Instantiate(railGunBeam, transform.position, Quaternion.identity);
                    _beam.transform.rotation = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z + 90);
                    _beam.GetComponent<RailGunBeam>().Owner = gameObject;
                    myAmmo--;

                }


                railGunCharge.SetActive(false);
                audioSource1.Stop();

            }

        }

    }


    IEnumerator ChargeUp()
    {
        yield return new WaitForSeconds(0.1f);
        chargeUp = false;
    }

    public void SetWeapon(Gun weapon)
    {
        myGun = weapon;
        myAmmo = weapon.ammo;
        weaponVolume = weapon.weaponVolume;
        timer = 0;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine((Vector2)transform.position, (Vector2)transform.position + (Vector2)(transform.up * railGunDistance));
    }


}
