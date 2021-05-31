using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
[RequireComponent(typeof(NavMeshAgent))]
public class wanderScript : MonoBehaviour
{
    public float wonderAmount;
    public float tolerance;

    private Vector3 start;
    private NavMeshAgent agent;
    private Vector3 nextPoint;
    // Start is called before the first frame update
    void Start()
    {
        start = transform.position;
        agent = GetComponent<NavMeshAgent>();
        nextPoint = generatePoint();
        agent.SetDestination(nextPoint);
    }

    // Update is called once per frame
    void Update()
    {
        if(isTolerated(nextPoint))
        {
            nextPoint = generatePoint();
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
    private Vector3 generatePoint()
    {
        float X = Random.Range(start.x - wonderAmount, start.x + wonderAmount);
        float Y = transform.position.y;
        float Z = Random.Range(start.z - wonderAmount, start.z + wonderAmount);

        return new Vector3(X, Y, Z);
    }
}
