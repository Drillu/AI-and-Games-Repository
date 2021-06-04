using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
[RequireComponent(typeof(NavMeshAgent))]
public class wanderScript : MonoBehaviour
{
    public float stopDistance;
    public float tolerance;

    private Vector3 start;
    private NavMeshAgent agent;
    private Vector3 nextPoint;
    public bool canGo = true;
    // Start is called before the first frame update
    private void Awake()
    {
        Prisoner pris = GetComponent<Prisoner>();
        if (pris != null)
        {
            pris.range = stopDistance;
        }
    }
    void Start()
    {
        start = transform.position;
        agent = GetComponent<NavMeshAgent>();
        nextPoint = GetRandomPoint(transform.position, 10000);
        agent.SetDestination(nextPoint);
    }

    // Update is called once per frame
    void Update()
    {
        if(isTolerated(nextPoint) && canGo)
        {
            nextPoint = GetRandomPoint(transform.position, 10000);
            agent.SetDestination(nextPoint);
        }
    }
    private bool isTolerated(Vector3 point)
    {
        if(Vector3.Distance(transform.position, point) < tolerance)
        {
            return true;
        }
        return false;
    }
    // Get Random Point on a Navmesh surface
    public static Vector3 GetRandomPoint(Vector3 center, float maxDistance)
    {
        // Get Random Point inside Sphere which position is center, radius is maxDistance
        Vector3 randomPos = Random.insideUnitSphere * maxDistance + center;

        NavMeshHit hit; // NavMesh Sampling Info Container

        // from randomPos find a nearest point on NavMesh surface in range of maxDistance
        NavMesh.SamplePosition(randomPos, out hit, maxDistance, NavMesh.AllAreas);

        return hit.position;
    }
    public void stopIt()
    {
        agent.ResetPath();
        canGo = false;
    }
    public void startIt()
    {
        nextPoint = GetRandomPoint(transform.position, 10000);
        agent.SetDestination(nextPoint);
        canGo = true;
    }
}
