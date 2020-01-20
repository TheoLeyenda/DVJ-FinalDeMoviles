using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    // Start is called before the first frame update
    public ParticleSystem[] explotionParticle;
    public StoneMonster shooter;
    private bool inExplotion;
    public float timeLife;
    public float auxTimeLife;

    public MeshRenderer meshRenderer;
    public SphereCollider sphereCollider;
    public TrailRenderer trailRenderer;
    public AudioSource audioSource;

    private void OnDisable()
    {
        inExplotion = false;
        timeLife = auxTimeLife;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        sphereCollider.enabled = true;
        meshRenderer.enabled = true;
        trailRenderer.enabled = true;
        audioSource.volume = 0.5f;
    }
    public void Implulse(float initialVelocity)
    {
        GetComponent<Rigidbody>().AddForce(transform.forward * initialVelocity, ForceMode.Impulse);
    }
    private void Update()
    {
        CheckDead();
    }
    public void CheckDead()
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
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Construccion")
        {
            int indexParticleSystem = Random.Range(0, explotionParticle.Length);
            Wall wall = other.GetComponent<Wall>();
            Construction construction = wall.construction;
            construction.life = construction.life - shooter.DamageProjectileConstructions;
            explotionParticle[indexParticleSystem].Play();
            inExplotion = true;
            sphereCollider.enabled = false;
            meshRenderer.enabled = false;
            trailRenderer.enabled = false;
            trailRenderer.Clear();
            timeLife = explotionParticle[indexParticleSystem].main.duration;
            audioSource.volume = 0f;
        }
        else if(other.tag == "Wall")
        {
            int indexParticleSystem = Random.Range(0, explotionParticle.Length);
            Wall wall = other.GetComponent<Wall>();
            Construction construction = wall.construction;
            construction.life = construction.life - shooter.DamageProjectileConstructions;
            explotionParticle[indexParticleSystem].Play();
            inExplotion = true;
            sphereCollider.enabled = false;
            meshRenderer.enabled = false;
            trailRenderer.enabled = false;
            trailRenderer.Clear();
            timeLife = explotionParticle[indexParticleSystem].main.duration;
            audioSource.volume = 0f;
        }
    }
}
