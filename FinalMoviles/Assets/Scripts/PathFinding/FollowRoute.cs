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
    [SerializeField]
    private int indexDataRoute;
    [HideInInspector]
    public List<GameObject> pathPoints;
    public bool dontRestartIndex;

    // ES EL NUMERO DE DataRoute que se debe buscar para interactuar(esto se setea desde el generador de enemigos).
    public int numRoute;

    private void Awake()
    {
        pathPoints = new List<GameObject>();
        if (!dontRestartIndex)
        {
            indexDataRoute = 0;
        }
        agent = GetComponent<NavMeshAgent>();
        if (agent == null && navMeshAgent != null)
        {
            agent = navMeshAgent;
        }
        //finishPoint = null;
    }
    private void OnEnable()
    {
        if (!dontRestartIndex)
        {
            OnFollowRoute();
        }
    }
    void Start()
    {
        FindGoDataRoute();
        if (!dontRestartIndex)
        {
            OnFollowRoute();
        }
    }
    public void FindGoDataRoute()
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
        Debug.Log(dataRoute);
    }
    void Update()
    {
        if (agent != null)
        {
            if (finishPoint != null && agent.enabled)
            {
                agent.SetDestination(finishPoint.transform.position);
            }
        }
    }
    public void OnFollowRoute()
    {
        generatePath();
        if (dataRoute != null)
        {
            //Debug.Log(!dontRestartIndex);
            if (!dontRestartIndex)
            {
                indexDataRoute = 0;
            }
            finishPoint = pathPoints[indexDataRoute];
        }
    }
    public void SetFinishPoint(GameObject _finishPoint)
    {
        finishPoint = _finishPoint;
    }
    public GameObject GetFinishPoint()
    {
        return finishPoint;
    }
    public void generatePath()
    {
        if (dataRoute != null)
        {
            for (int i = 0; i < dataRoute.curves.Length; i++)
            {
                int random = Random.Range(0, dataRoute.curves[i].points.Length);
                pathPoints.Insert(i, dataRoute.curves[i].points[random]);
            }
            Debug.Log("inicialice el data route");
        }
    }
    public bool CheckFinishRoute()
    {
        if (finishPoint != null && pathPoints.Count > 0)
        {
            Vector3 a = finishPoint.transform.position - transform.position;
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
        else if (pathPoints.Count <= 0)
        {
            generatePath();
            return false;
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
    public int GetIndexRoute()
    {
        return indexDataRoute;
    }
    public void SetIndexRoute(int _indexRoute)
    {
        indexDataRoute = _indexRoute;
    }
}
