using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteoro : MonoBehaviour
{
    public ParticleSystem particleSystem;
    public GameObject explotionObject;
    public GameObject meteoroObject;
    public float speed;
    public float deleyDurationExplotion;
    public float auxDelayDurationExplotion;
    public float timeLife;
    public float auxTimeLife;
    private bool inExplotion = false;
    private PoolObject poolObject;
    public int Damage;
    public Vector3 target;
    public Rigidbody rig;
    private void Start()
    {
        poolObject = GetComponent<PoolObject>();
    }
    private void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.tag != "Meteoro")
        {
            explotionObject.SetActive(true);
            meteoroObject.SetActive(false);
            particleSystem.Play();
            inExplotion = true;
        }
    }
    private void OnDisable()
    {
        inExplotion = false;
        explotionObject.SetActive(false);
        meteoroObject.SetActive(true);
        deleyDurationExplotion = auxDelayDurationExplotion;
        timeLife = auxTimeLife;
        rig.velocity = Vector3.zero;
        rig.angularVelocity = Vector3.zero;
    }
    public void Movement() { 
        transform.LookAt(target);
        transform.position = transform.position + transform.forward * speed * Time.deltaTime;
    }
    private void Update()
    {
        Movement();
        if (inExplotion)
        {
            if (deleyDurationExplotion > 0)
            {
                deleyDurationExplotion = deleyDurationExplotion - Time.deltaTime;
            }
            else
            {

                //poolObject.Recycle();
                gameObject.SetActive(false);
            }
        }
        if (timeLife > 0)
        {
            timeLife = timeLife - Time.deltaTime;
        }
        else if (timeLife <= 0)
        { 
            timeLife = auxTimeLife;
            //poolObject.Recycle();
            gameObject.SetActive(false);
        }
    }
}
