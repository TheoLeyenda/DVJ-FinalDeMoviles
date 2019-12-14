using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    //public WeaponsRegistry.Registry currentWeapon;
    private Gun currentGun;
    public List<Gun> equipedGuns;
    private int indexGun;

    void Start()
    {
        //currentWeapon = WeaponsRegistry.Registry.Pistol;
        indexGun = 0;
        currentGun = equipedGuns[indexGun];
    }
    // Update is called once per frame
    void Update()
    {
#if !UNITY_ANDROID
        CheckInput();
#endif
    }
    public void CheckInput()
    {
        if (Input.GetButton("Fire"))
        {
            ShootGunEquiped();
        }
        if (Input.GetButton("ReloadButton"))
        {
            ReloadCurrentGun();
        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            SwichtGunUp();
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            SwichtGunDown();
        }
    }
    public void SwichtGunUp()
    {
        indexGun++;
        if(indexGun >= equipedGuns.Count)
        {
            indexGun = 0;
        }
        currentGun = equipedGuns[indexGun];
    }
    public void SwichtGunDown()
    {
        indexGun--;
        if (indexGun < 0)
        {
            indexGun = equipedGuns.Count - 1;
        }
        currentGun = equipedGuns[indexGun];
    }
    public void ShootGunEquiped()
    {
        if (currentGun.GetBulletInCharger() > 0 && currentGun.GetEnableShoot())
        {
            if (currentGun.rateOfShoot <= 0)
            {
                switch (currentGun.typeGun)
                {
                    case Gun.TypeGun.Automatic:
                        if (Input.GetButton("Fire"))
                            currentGun.Shoot(Gun.TypeGun.Automatic);
                        break;
                    case Gun.TypeGun.Blast:
                        if (Input.GetButtonDown("Fire"))
                            currentGun.Shoot(Gun.TypeGun.Blast);
                        break;
                    case Gun.TypeGun.Semiautomatic:
                        if (Input.GetButtonDown("Fire"))
                            currentGun.Shoot(Gun.TypeGun.Semiautomatic);
                        break;
                }
                currentGun.SetEnableShoot(false);
                currentGun.rateOfShoot = currentGun.GetAuxRateOfShoot();
            }
        }
    }
    public void ReloadCurrentGun()
    {
        currentGun.Reload();
    }
}
