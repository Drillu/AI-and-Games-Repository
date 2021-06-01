using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
[RequireComponent(typeof(NavMeshAgent))]
public class guardMove : MonoBehaviour
{
    public List<Transform> wayPoints;
    public float wayPointTolerance;
    public GameObject Player;
    public bool IsPlayerInWiev;

    [Header("Vision")]
    public float radius;
    [Range(0, 360)]
    public float angle;
    public float followDelay;
    public LayerMask targetMask;
    public LayerMask obstructionMask;

    private int currentPoint;
    private NavMeshAgent agent;
    //Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        currentPoint = 0;
        if (Player == null)
        {
            Player = GameObject.FindGameObjectWithTag("Player");
        }
        StartCoroutine(FOVRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        if (IsPlayerInWiev)
        {
            agent.SetDestination(Player.transform.position);
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
    private IEnumerator FOVRoutine()
    {
        float delay = 0.2f;
        WaitForSeconds wait = new WaitForSeconds(delay);

        while (true)
        {
            yield return wait;
            FielIFViewCheck();
        }
    }

    private void FielIFViewCheck()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask);

        if (rangeChecks.Length != 0)
        {
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.position, directionToTarget) < angle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))
                {
                    IsPlayerInWiev = true;
                }
                else
                {
                    IsPlayerInWiev = false;
                }
            }
            else
                IsPlayerInWiev = false;
        }
        else if (IsPlayerInWiev)
            StartCoroutine(stopFollow());
    }

    private IEnumerator stopFollow()
    {
        yield return new WaitForSeconds(followDelay);
        IsPlayerInWiev = false;
    }
}
