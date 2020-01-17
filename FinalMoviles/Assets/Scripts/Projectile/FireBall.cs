using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    // Start is called before the first frame update
    public ParticleSystem explotionParticle;
    public StoneMonster shooter;
    private bool inExplotion;
    public float timeLife;
    public float auxTimeLife;

    private void OnDisable()
    {
        inExplotion = false;
        timeLife = auxTimeLife;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
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

        if(inExplotion && !explotionParticle.isPlaying)
        {
            timeLife = 0f;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Construccion")
        {
            Wall wall = other.GetComponent<Wall>();
            Construction construction = wall.construction;
            construction.life = construction.life - shooter.DamageProjectileConstructions;
            explotionParticle.Play();
            inExplotion = true;
        }
    }
}
