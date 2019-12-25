using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class FollowRoute : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject finishPoint;
    private NavMeshAgent agent;
    public int countFinishPoints;
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        finishPoint = null;
    }
    private void OnEnable()
    {
        OnFollowRoute();
    }
    void Start()
    {
        OnFollowRoute();
    }
    void OnFollowRoute()
    {
        finishPoint = null;
        switch (Random.Range(0, countFinishPoints))
        {
            case 0:
                finishPoint = GameObject.Find("FinishPoint");
                break;
            case 1:
                finishPoint = GameObject.Find("FinishPoint 1");
                break;
            case 2:
                finishPoint = GameObject.Find("FinishPoint 2");
                break;
            case 3:
                finishPoint = GameObject.Find("FinishPoint 3");
                break;
            case 4:
                finishPoint = GameObject.Find("FinishPoint 4");
                break;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (finishPoint != null)
        {
            agent.SetDestination(finishPoint.transform.position);
        }
    }
    public bool CheckFinishRoute()
    {
        if (finishPoint != null)
        {
            Vector3 a = finishPoint.transform.position - transform.position;
            //Debug.Log("Magnitud: " + a.magnitude);
            //Debug.Log("distancia para detenerse: " + agent.stoppingDistance);
            if (a.magnitude <= agent.stoppingDistance)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }
    public NavMeshAgent GetAgent()
    {
        return agent;
    }
}
