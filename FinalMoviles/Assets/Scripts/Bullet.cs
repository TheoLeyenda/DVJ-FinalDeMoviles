using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    public float velocity;
    public float damage;
    public float timeLife;
    public float auxTimeLife;
    public Pool poolBullet;
    private PoolObject poolObjectBullet;
    public ParticleSystem BloodEffect;
    public GameObject modelBullet;
    private bool enableMovement;
    public Rigidbody rig;
    public Quaternion direccionOfShoot;
    //public Vector3 direccionOfSpawn;
    // Update is called once per frame
    private void Start()
    {
        enableMovement = true;
    }
    void Update()
    {
        if (!enableMovement)
        {
            rig.velocity = Vector3.zero;
        }
    }
    /*public void Movement()
    {
        transform.position = transform.position + transform.forward * Time.deltaTime * velocity;
    }*/
    public void On()
    {
        direccionOfShoot = transform.rotation;
        rig.velocity = Vector3.zero;
        rig.angularVelocity = Vector3.zero;

        enableMovement = true;
        poolObjectBullet = GetComponent<PoolObject>();
        rig.AddForce(transform.forward * velocity, ForceMode.Impulse);
        modelBullet.SetActive(true);
        BloodEffect.gameObject.SetActive(false);
        timeLife = auxTimeLife;
    }
    public void CheckTimeLife()
    {
        if (timeLife > 0)
        {
            timeLife = timeLife - Time.deltaTime;
        }
        else if(timeLife <= 0)
        {
            if (poolObjectBullet != null)
            {
                poolObjectBullet.Recycle();
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "Enemy":
                //BloodEffect.transform.rotation = new Quaternion(transform.rotation.x, transform.rotation.y + 180, transform.rotation.z, transform.rotation.w);
                transform.rotation = direccionOfShoot;
                BloodEffect.gameObject.SetActive(true);
                BloodEffect.Play();
                timeLife = BloodEffect.main.startLifetime.constant + 0.2f;
                modelBullet.SetActive(false);
                enableMovement = false;
                break;
        }
    }
}
