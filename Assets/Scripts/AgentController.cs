using UnityEngine;
using PathCreation;
using UnityEngine.AI;
using PathSystem;
using Oculus.Interaction;
using UnityEngine.Splines;
using System.Collections;
using UnityEditor;

[RequireComponent(typeof(PNJMain))]
[RequireComponent(typeof(Rigidbody))]
public class AgentController : MonoBehaviour
{
    PathCheckpoint currentCheckpoint;

    float currentProgress;

    Vector3 nextPoint;

    public float JumpSpeed , stoppingDistance , walkSpeed , gravity;

    Rigidbody rb;

    float currentSpeed;

    public bool debug;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentCheckpoint = PathManager.instance.BegininPath;
        StartNewPath();
    }

    void StartNewPath()
    {
        switch (currentCheckpoint.type)
        {
            case PathType.Walk:
                currentSpeed = walkSpeed;
                StartCoroutine(ProcessWalkPathing());
                break;
            case PathType.Jump:
                currentSpeed = JumpSpeed;
                StartCoroutine(ProcessJumpPathing());
                break;
            case PathType.Swim:
                break;
            case PathType.Fall:
                break;
            case PathType.Fly:
                break;
            case PathType.Elevator:
                break;
            case PathType.Ladder:
                break;
            case PathType.None:
                break;
        }
    }

    void TurnToPoint(bool allAxes = true)
    {
        Vector3 pointPos = new Vector3(nextPoint.x, allAxes?nextPoint.y:transform.position.y,nextPoint.z);

        transform.LookAt(pointPos);
    }

    Vector3 TakeXZOfVector(Vector3 vector , bool conservGravity = false)
    {
        return new Vector3(vector.x, conservGravity ? rb.velocity.y : 0, vector.z);
    }

    void GoToPoint()
    {
        Vector3 Direction = (nextPoint - transform.position).normalized;
        rb.velocity = Direction * currentSpeed * Time.deltaTime;
    }

    private void OnDrawGizmos()
    {
        if (debug)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(TakeXZOfVector(nextPoint), 0.01f);
            Gizmos.DrawWireSphere(TakeXZOfVector(transform.position), 0.01f);
            Gizmos.DrawLine(TakeXZOfVector(nextPoint), TakeXZOfVector(transform.position));
        }
    }

    #region Points detection type
    bool CheckIfPointIsReached(PathType type)
    {
        if ((transform.position - nextPoint).magnitude < stoppingDistance)
            return true;
        else
            return false;

/*        switch (type)
        {
            case PathType.Walk:
                return isWalkPointReached();
            case PathType.Jump:
                return isSpacePointReached();
            case PathType.Swim:
                return isSpacePointReached();
            case PathType.Fall:
                break;
            case PathType.Fly:
                break;
            case PathType.Elevator:
                break;
            case PathType.Ladder:
                break;
        }
        return false;*/
    }

    bool isWalkPointReached()
    {
        Vector3 XYPos = new Vector3(transform.position.x, 0, transform.position.z);

        Vector3 XYPosPoint = new Vector3(nextPoint.x, 0, nextPoint.z);
        if ((XYPos - XYPosPoint).magnitude <= stoppingDistance)
            return true;
        else
            return false;
    }

    bool isSpacePointReached()
    {
        if ((transform.position - nextPoint).magnitude < stoppingDistance)
            return true;
        else 
            return false;
    }

    #endregion

    #region Jump
    IEnumerator ProcessJumpPathing()
    {
        rb.useGravity = false;
        while (currentProgress < 1)
        {
            if (nextPoint == Vector3.zero)
            {
                nextPoint = GetNextPos();
            }
            if (CheckIfPointIsReached(currentCheckpoint.type))
            {
                nextPoint = GetNextPos();
            }
            GoToPoint();

            yield return new WaitForEndOfFrame();
        }
        rb.useGravity = true;
        LastPointOfPathReached();
    }
    #endregion

    #region Walk
    IEnumerator ProcessWalkPathing()
    {
        while (currentProgress < 1)
        {
            if (nextPoint == Vector3.zero)
            {
               nextPoint = GetNextPos();
            }
            if (CheckIfPointIsReached(currentCheckpoint.type))
            {
                nextPoint = GetNextPos();
            }

            TurnToPoint(false);
            GoToPoint();
            yield return new WaitForEndOfFrame();
        }
        LastPointOfPathReached();
    }
    #endregion

    Vector3 GetNextPos()
    {
        return currentCheckpoint.GetNextPos(currentProgress,out currentProgress);
    }

    void LastPointOfPathReached()
    {
        nextPoint = Vector3.zero;
        currentProgress = 0;

        GetNewCheckpoint();
        if (currentCheckpoint)
            StartNewPath();
        else
            print("no more checkpoints");
    }

    void GetNewCheckpoint()
    {
        if (currentCheckpoint)
            currentCheckpoint = currentCheckpoint.GetNextPathCheckpoint();
    }

    /*public PNJMain main {  get; private set; }

    public NavMeshAgent agent;
    Vector3[] pathPoint;
    int index;
    PathCheckpoint currentPathCheck;
    bool endingPath = false;
    public float jumpSpeed = 0.05f;
    bool isDeath = false;

    [SerializeField]
    string collisionTag;

    private void Awake()
    {
        main = GetComponent<PNJMain>();
    }

    void Update()
    {
        if (agent.enabled == true)
        {
            if (agent.remainingDistance <= agent.stoppingDistance && agent.hasPath)
            {
                NextDestination();
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, pathPoint[index], jumpSpeed);
            if (transform.position == pathPoint[index])
            {
                NextDestination();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.transform.tag);
        if (other.transform.tag == collisionTag)
            if (main)
                main.IAmDead();
    }

    void NextDestination()
    {
        if (index < pathPoint.Length - 1)
        {
            if (agent.enabled == true)
            {
                bool get = agent.SetDestination(pathPoint[index]);
                index++;
            }
        }
        else
        {
            if (endingPath == false)
            {
                ChangePath(currentPathCheck.nextPath[GetPathIndex()].pathCheck);
            }else
            {
                if (isDeath)
                {
                    if(main)
                        main.IAmDead();
                    //do something when dead
                }
            }
        }
    }

    int GetPathIndex()
    {
        if (currentPathCheck.nextPath.Length == 1)
        {
            return 0;
        }

        int pathIndex = -1;
        for (int i = 0; i < currentPathCheck.nextPath.Length; i++)
        {
            if (currentPathCheck.nextPath[i].pathCheck.activated == true)
            {
                pathIndex = i;
                break;
            }
        }

        if (pathIndex != -1)
        {
            return pathIndex;
        }
        else
        {
            for (int i = 0; i < currentPathCheck.nextPath.Length; i++)
            {
                if (currentPathCheck.nextPath[i].pathCheck.pathType == default)
                {
                    pathIndex = i;
                    break;
                }
            }
            return pathIndex != -1 ? pathIndex : 0;
        }
    }

    public void ChangePath(PathCheckpoint pathCheck)
    {
        currentPathCheck = pathCheck;
        PathCreator pathCreator = pathCheck.path;
        pathPoint = pathCreator.path.localPoints;
        index = 0;
        SetStatesFromPathType(pathCheck.pathType);
    }

    void SetStatesFromPathType(PathType pathType)
    {
        switch (pathType)
        {
            case PathType.DEFAULT:
                SetMoveWithNavMesh();
                break;

            case PathType.END:
                SetMoveWithNavMesh();
                endingPath = true;
                break;

            case PathType.JUMP:
                SetMoveWithoutNavMesh();
                break;

            case PathType.DEATH:
                SetMoveWithoutNavMesh();
                endingPath = true;
                isDeath = true;
                break;
        }
    }

    void SetMoveWithNavMesh()
    {
        agent.enabled = true;
        agent.isStopped = false;
        agent.SetDestination(pathPoint[index]);
    }

    void SetMoveWithoutNavMesh()
    {
        agent.isStopped = true;
        agent.enabled = false;
    }*/
}
