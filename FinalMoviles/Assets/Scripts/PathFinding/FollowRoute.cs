using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class FollowRoute : MonoBehaviour
{
    // Start is called before the first frame update
    public NavMeshAgent navMeshAgent;
    [SerializeField]
    private GameObject finishPoint;
    private DataRoute dataRoute;
    [SerializeField]
    private GameObject goDataRoute;
    private NavMeshAgent agent;
    private int indexDataRoute;
    private List<GameObject> pathPoints;

    // ES EL NUMERO DE DataRoute que se debe buscar para interactuar(esto se setea desde el generador de enemigos).
    public int numRoute;

    private void Awake()
    {
        pathPoints = new List<GameObject>();
        indexDataRoute = 0;
        agent = GetComponent<NavMeshAgent>();
        if (agent == null && navMeshAgent != null)
        {
            agent = navMeshAgent;
        }
        finishPoint = null;
    }
    private void OnEnable()
    {
        OnFollowRoute();
    }
    void Start()
    {
        if (numRoute == 0 || numRoute == 1)
        {
            goDataRoute = GameObject.Find("DataRoute");
        }
        else
        {
            goDataRoute = GameObject.Find("DataRoute " + numRoute);
        }
        if (goDataRoute != null)
        {
            dataRoute = goDataRoute.GetComponent<DataRoute>();
        }
        OnFollowRoute();
    }

    void Update()
    {
        if (finishPoint != null)
        {
            agent.SetDestination(finishPoint.transform.position);
        }
    }
    void OnFollowRoute()
    {
        generatePath();
        if (dataRoute != null)
        {
            indexDataRoute = 0;
            finishPoint = pathPoints[indexDataRoute];
        }
    }
    void generatePath()
    {
        if (dataRoute != null)
        {
            for (int i = 0; i < dataRoute.curves.Length; i++)
            {
                int random = Random.Range(0, dataRoute.curves[i].points.Length);
                pathPoints.Insert(i, dataRoute.curves[i].points[random]);
            }
        }
    }
    public bool CheckFinishRoute()
    {
        if (finishPoint != null)
        {
            Vector3 a = finishPoint.transform.position - transform.position;
            /*if (finishPoint == pathPoints[pathPoints.Count - 1])
            {
                Debug.Log(a.magnitude <= agent.stoppingDistance);
                Debug.Log("a.magnitude: "+a.magnitude);
                Debug.Log("agent.stoppingDistance: " + agent.stoppingDistance);
            }*/
            if (a.magnitude <= agent.stoppingDistance)
            {
                if (finishPoint == pathPoints[pathPoints.Count - 1])
                {
                    return true;
                }
                else
                {
                    indexDataRoute++;
                    finishPoint = pathPoints[indexDataRoute];
                    return false;
                }
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
