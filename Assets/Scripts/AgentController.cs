using UnityEngine;
using PathCreation;
using UnityEngine.AI;
using PathSystem;

public class AgentController : MonoBehaviour
{
    public NavMeshAgent agent;
    Vector3[] pathPoint;
    int index;
    PathCheckpoint currentPathCheck;
    bool endingPath = false;
    public float jumpSpeed = 0.05f;
    bool isDeath = false;

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

    void NextDestination()
    {
        if (index < pathPoint.Length - 1)
        {
            index++;
            if (agent.enabled == true)
            {
                agent.SetDestination(pathPoint[index]);
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
                    Destroy(this.gameObject);
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
    }
}
