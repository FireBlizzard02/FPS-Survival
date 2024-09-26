using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private WeaponManager weaponManager;

    private float fireInterval=0.2f;
    public float damage = 20f;
    private float currentFireTime;
    private void Awake()
    {
        weaponManager = GetComponent<WeaponManager>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        WeaponShoot();
    }

    private void WeaponShoot()
    {
        if (weaponManager.GetCurrentSelectedWeapon().weaponFireType == WeaponFireType.MULTIPLE)
        {
            if (Input.GetMouseButton(0))
            {
                if (currentFireTime <= 0)
                {
                    weaponManager.GetCurrentSelectedWeapon().ShootAnimation();
                    BulletFire();
                    currentFireTime = fireInterval;
                }
                else
                {
                    currentFireTime -= Time.deltaTime;
                }
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                weaponManager.GetCurrentSelectedWeapon().ShootAnimation();
                BulletFire();
            }
        }

    }
    private void BulletFire()
    {
        RaycastHit hit;

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit))
        {

            if (hit.transform.tag == "Enemy")
            {
                Debug.Log("Hit enemy: " + hit.transform.name);
                hit.transform.GetComponent<HealthScript>().ApplyDamage(damage);
            }

        }
    }
}
