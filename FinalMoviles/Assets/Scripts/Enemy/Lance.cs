using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DarkTreeFPS;

public class Lance : MonoBehaviour
{
    // Start is called before the first frame update
    public float timeLife = 5;
    public float auxTimeLife = 5;
    public int Damage = 5;
    public int DamageConstruction = 2;
    public float initialVelocity = 80;
    public Rigidbody rig;
    public TrailRenderer trailRenderer;
    
    private void OnDisable()
    {
        trailRenderer.Clear();
        timeLife = auxTimeLife;
        rig.velocity = Vector3.zero;
        rig.angularVelocity = Vector3.zero;
    }
    // Update is called once per frame
    public void CheckTimeLife()
    {
        if (timeLife > 0)
        {
            timeLife = timeLife - Time.deltaTime;
        }
        else if (timeLife <= 0)
        {
            gameObject.SetActive(false);
        }
    }
    private void Update()
    {
        CheckTimeLife();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Player")
        {
            PlayerStats player = collision.gameObject.GetComponent<PlayerStats>();
            player.health = player.health - Damage;
            gameObject.SetActive(false);
        }
        else if(collision.gameObject.tag != "Inside" && collision.gameObject.tag != "Wall" && collision.transform.tag != "Enemy" && collision.transform.tag != "Shild" && collision.transform.tag != "Construccion" && collision.transform.tag != "Lance")
        {
            gameObject.SetActive(false);
        }
        if (collision.gameObject.tag == "Wall" || collision.gameObject.tag == "Construccion")
        {
            Construction construction = collision.gameObject.GetComponent<Wall>().construction;
            /*if (construction.GetEnableCameraShake())
            {
                StartCoroutine(construction.player.cameraShake.Shake(construction.player.durationCameraShake, construction.player.magnitudeCameraShake));
            }*/
            construction.life = construction.life - DamageConstruction;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Wall" || other.tag == "Construccion")
        {
            Construction construction = other.GetComponent<Wall>().construction;
            /*if (construction.GetEnableCameraShake())
            {
                StartCoroutine(construction.player.cameraShake.Shake(construction.player.durationCameraShake, construction.player.magnitudeCameraShake));
            }*/
            construction.life = construction.life - DamageConstruction;
        }
    }
}
