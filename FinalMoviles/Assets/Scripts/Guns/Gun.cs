using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    // Start is called before the first frame update
    public int countBullets;
    protected int bulletInCharger;
    protected bool enableShoot;
    protected float auxRateOfShoot;
    public float rateOfShoot;
    public Pool poolBullets;
    public GameObject GeneratorBullet;
    public TypeGun typeGun;


    public enum TypeGun
    {
        Semiautomatic,
        Automatic,
        Blast,
    }
    void Start()
    {
        auxRateOfShoot = rateOfShoot;
        enableShoot = true;
        rateOfShoot = 0;
        bulletInCharger = countBullets;
    }

    private void Update()
    {
        //CheckShoot();
        CheckRateOfShoot();
        //CheckReload();
    }
    
    public virtual void Shoot(TypeGun _typeGun)
    {
        GameObject go = poolBullets.GetObject();
        Bullet bullet = go.GetComponent<Bullet>();
        switch (_typeGun)
        {
            case TypeGun.Automatic:
                break;
            case TypeGun.Blast:
                break;
            case TypeGun.Semiautomatic:
                go.transform.position = GeneratorBullet.transform.position;
                go.transform.rotation = GeneratorBullet.transform.rotation;
                //bullet.direccionOfSpawn = GeneratorBullet.transform.forward;
                bullet.On();
                bulletInCharger--;
                break;
        }
    }
    
    public void CheckRateOfShoot()
    {
        if (rateOfShoot > 0)
        {
            rateOfShoot = rateOfShoot - Time.deltaTime;
        }
        else if (rateOfShoot <= 0)
        {
            enableShoot = true;
        }
    }
    public virtual void Reload()
    {
        bulletInCharger = countBullets;
    }
    public int GetBulletInCharger()
    {
        return bulletInCharger;
    }
    public bool GetEnableShoot()
    {
        return enableShoot;
    }
    public float GetAuxRateOfShoot()
    {
        return auxRateOfShoot;
    }
    public void SetEnableShoot(bool _enableShoot)
    {
        enableShoot = _enableShoot;
    }
}

// Update is called once per frame
