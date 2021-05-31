using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
[RequireComponent(typeof(NavMeshAgent))]
public class guardMove : MonoBehaviour
{
    public List<Transform> wayPoints;
    public float wayPointTolerance;
    public Transform Player;
    public bool IsPlayerInWiev;    
        
    private int currentPoint;
    private NavMeshAgent agent;
    //Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        currentPoint = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(IsPlayerInWiev)
        {
            agent.SetDestination(Player.position);
        }
        else
        {
            if (Vector3.Distance(transform.position, wayPoints[currentPoint].position) < wayPointTolerance)
                currentPoint++;
            if (currentPoint >= wayPoints.Count)
                currentPoint = 0;

            agent.SetDestination(wayPoints[currentPoint].position);
        }
    }
}
