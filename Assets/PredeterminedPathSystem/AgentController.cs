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

    [SerializeField]
    bool canWalk;

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
        rb.velocity = Direction * currentSpeed;
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
        /*        if ((transform.position - nextPoint).magnitude < stoppingDistance)
                    return true;
                else
                    return false;
        */
        switch (type)
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
        return false;
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
            else if (CheckIfPointIsReached(currentCheckpoint.type))
            {
                nextPoint = GetNextPos();
            }
            
            TurnToPoint(false);
            if(canWalk)
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
}
