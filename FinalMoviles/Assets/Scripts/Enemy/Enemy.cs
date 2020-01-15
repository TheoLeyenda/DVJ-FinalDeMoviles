using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    public enum TypeEnemy
    {
        Normal, // van normalmente al punto final del recorrido
        Runner, // Corren al punto final del recorrido
        Fighter,// Al ver al jugador lo atacan
        ConstructionDestroyer, //Al ver una construccion la atacan
    }
    public int Damage;
    public ParticleSystem blood;
    public float scalerBloodVar;
    public GameObject EnemyPrefab;
    public FollowRoute auxFollowRoute;
    private FollowRoute followRoute;
    public float speed;
    protected float auxSpeed;
    public float acceletartion;
    public float life = 100;
    public bool inPool;
    public TypeEnemy typeEnemy;
    public string nameEnemy;
    private bool finishRoute;


    public static event Action<Enemy> OnDieAction;
    void Start()
    {
#if UNITY_ANDROID
        speed = speed / 2;
#endif
        finishRoute = false;
        acceletartion = acceletartion * 10;
        auxSpeed = speed;
        followRoute = GetComponent<FollowRoute>();
        if (followRoute == null && auxFollowRoute != null)
        {
            followRoute = auxFollowRoute;
            followRoute.GetAgent().speed = speed;
            followRoute.GetAgent().acceleration = acceletartion;
        }

    }

    // Update is called once per frame
    protected virtual void Update()
    {
        CheckFinishRouteEnemy();
        CheckDieEnemy();
    }
    public void CheckFinishRouteEnemy()
    {
        if (followRoute.CheckFinishRoute() || finishRoute)
        {
            //SI ANDA MAL LA DESAPARICION O APARICION DEL ENEMIGO PROGRAMAR EL TEMA DEL RECICLADO DEL POOL
            finishRoute = false;
            if (EnemyPrefab != null)
            {
                if (OnDieAction != null)
                    OnDieAction(this);
                EnemyPrefab.SetActive(false);
            }
            else
            {
                if (OnDieAction != null)
                    OnDieAction(this);
                gameObject.SetActive(false);
            }
        }
    }
    public void CheckDieEnemy()
    {
        if (life <= 0)
        {
            //SI ANDA MAL LA DESAPARICION O APARICION DEL ENEMIGO PROGRAMAR EL TEMA DEL RECICLADO DEL POOL
            DieEnemy();
        }
    }
    public void DieEnemy()
    {
        if (EnemyPrefab != null)
        {
            if (OnDieAction != null)
                OnDieAction(this);
            EnemyPrefab.SetActive(false);
        }
        else
        {
            if (OnDieAction != null)
                OnDieAction(this);
            gameObject.SetActive(false);
        }
    }
    public virtual void Attack() { }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "FinishPoint")
        {
            finishRoute = true;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "FinishPoint")
        {
            finishRoute = true;
        }
    }
}
