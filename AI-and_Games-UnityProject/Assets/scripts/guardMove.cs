using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
[RequireComponent(typeof(NavMeshAgent))]
public class guardMove : MonoBehaviour
{
    public GameObject thing;
    public List<Transform> wayPoints;
    public float wayPointTolerance;
    public GameObject Player;
    private Inventory inventory;
    public bool IsPlayerInWiev;
    private bool isPerquisitionTime;
    public float collisinDebugTime;
    public bool canTrack;
    public LayerMask PlayerLayer;
    public float collisionDistance;

    [Header("Vision")]
    public float radius;
    private float angle;
    public float speed;
    public Vector3 heightOffset;
    public Vector3 playerPos;
    public float followDelay;
    public LayerMask targetMask;
    public LayerMask obstructionMask;

    private int currentPoint;
    private NavMeshAgent agent;
    [SerializeField]
    private LineRenderer firstLine;

    [SerializeField]
    private LineRenderer secondLine;

    //Start is called before the first frame update
    void Start()
    {
        canTrack = true;
        agent = GetComponent<NavMeshAgent>();
        currentPoint = 0;
        if (Player == null)
        {
            Player = GameObject.FindGameObjectWithTag("Player");
        }
        firstLine.transform.localScale = new Vector3(1, 1, radius);
        secondLine.transform.localScale = new Vector3(1, 1, radius);
    }
    private void UpdateVisionLines()
    {
        firstLine.transform.rotation = Quaternion.Euler(0, angle, 0);
        firstLine.transform.position = transform.position + heightOffset;
        secondLine.transform.rotation = Quaternion.Euler(0, -angle, 0);
        secondLine.transform.position = transform.position + heightOffset;
    }

    // Update is called once per frame
    void Update()
    {
        agent.ResetPath();
        startRay();

        cancelIf();

        if (canTrack)
        {
            Collider[] player = Physics.OverlapSphere(transform.position, collisionDistance, PlayerLayer);
            if (player.Length != 0)
            {
                if (isPerquisitionTime)
                {
                    if (inventory.slots.Count == 0)
                    {
                        perquisitionManager.instance.EndPerquisition();
                        Debug.Log("Go on :)");
                    }
                    else
                    {
                        Debug.Log("caught");
                        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                    }
                    StartCoroutine(stopCollision());
                }
                else
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                }
            }
        }

        if (!isPerquisitionTime)
        {
            if (IsPlayerInWiev && canTrack)
            {
                agent.SetDestination(playerPos);
                
                //DrawNavMeshPath.path = agent.path.corners;
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
        else
        {
            agent.SetDestination(Player.transform.position);
        }
        if(playerPos != Vector3.zero && playerPos != Player.transform.position)
            thing.transform.position = playerPos;
        else
            thing.transform.position = new Vector3(0, -10, 0);

    }
    private void startRay()
    {
        angle += speed * Time.deltaTime;
        bool left = ViewRay(angle);
        bool right = ViewRay(-angle);
        if(right || left)
        {
            IsPlayerInWiev = true;
            Debug.Log("found");
        }
        else if(right && left)
        {
            IsPlayerInWiev = true;
            Debug.Log("found");
        }
        /*else
        {
            IsPlayerInWiev = false;
        }*/

        UpdateVisionLines();
    }
    private bool ViewRay(float angleIn)
    {
        Vector3 pos = transform.position + heightOffset;
        Vector3 rot = new Vector3(0, angleIn, 0);
        Debug.DrawRay(pos, Quaternion.Euler(rot) * Vector3.forward, Color.red);
        if (Physics.Raycast(pos, Quaternion.Euler(rot) * Vector3.forward, out RaycastHit hit, radius, targetMask))
        {
            playerPos = Player.transform.position;
            return true;
        }

        return false;
    }

    private void cancelIf()
    {
        if(Vector3.Distance(transform.position, playerPos) < wayPointTolerance)
        {
            Debug.Log("lost");
            IsPlayerInWiev = false;
            playerPos = Vector3.zero;
        }
    }

    private IEnumerator stopPerquisition(float time)
    {
        yield return new WaitForSeconds(time);
        isPerquisitionTime = false;
    }
    public void perquisition(bool isOn, float time)
    {
        if (isOn)
        {
            isPerquisitionTime = true;
            Debug.Log("it's perquisition time!");
            StartCoroutine(stopPerquisition(time));
        }
        else
        {
            StopCoroutine(stopPerquisition(time));
            isPerquisitionTime = false;
        }
    }
    private IEnumerator stopCollision()
    {
        canTrack = false;
        yield return new WaitForSeconds(collisinDebugTime);
        canTrack = true;
    }
}
