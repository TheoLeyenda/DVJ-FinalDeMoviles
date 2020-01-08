using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    
    void Start()
    {

        auxSpeed = speed;
        followRoute = GetComponent<FollowRoute>();
        if (followRoute == null && auxFollowRoute != null)
        {
            followRoute = auxFollowRoute;
        }
        followRoute.GetAgent().speed = speed;
        followRoute.GetAgent().acceleration = acceletartion;
    }

    // Update is called once per frame
    void Update()
    {
        CheckFinishRouteEnemy();
        CheckDeadEnemy();
    }
    public void CheckFinishRouteEnemy()
    {
        if (followRoute.CheckFinishRoute())
        {
            if (inPool)
            {
                //Escribir la programacion del Resiclado del pool y todo eso.
            }
            else
            {
                if (EnemyPrefab != null)
                {
                    EnemyPrefab.SetActive(false);
                }
                else
                {
                    gameObject.SetActive(false);
                }
            }
        }
    }
    public void CheckDeadEnemy()
    {
        if (life <= 0)
        {
            if (inPool)
            {
                //Escribir la programacion del Resiclado del pool y todo eso.
            }
            else
            {
                if (EnemyPrefab != null)
                {
                    EnemyPrefab.SetActive(false);
                }
                else
                {
                    gameObject.SetActive(false);
                }
            }
        }
    }
}
