using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    private FollowRoute followRoute;
    public float speed;
    protected float auxSpeed;
    public float acceletartion;
    public float life = 100;
    public bool inPool;
    
    void Start()
    {
        auxSpeed = speed;
        followRoute = GetComponent<FollowRoute>();
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
                gameObject.SetActive(false);
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
                gameObject.SetActive(false);
            }
        }
    }
}
