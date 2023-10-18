using UnityEngine;
using PathCreation;
using UnityEngine.AI;

public class AgentController : MonoBehaviour
{
    public NavMeshAgent agent;
    PathCreator pathCreator;
    Vector3[] pathPoint;
    int index;

    public Transform point;

    void Update()
    {
        if (agent.remainingDistance <= agent.stoppingDistance && agent.hasPath)
        {
            NextDestination();
            Debug.Log("Test");
        }
    }

    [ContextMenu("test")]
    void Test()
    {
        agent.SetDestination(point.position);
    }

    void NextDestination()
    {
        if (index < pathPoint.Length - 1)
        {
            index++;
            agent.SetDestination(pathPoint[index]);
        }
        else
        {
            // Finish
        }
    }

    public void ChangePath(PathCreator path)
    {
        pathCreator = path;
        pathPoint = pathCreator.path.localPoints;
        index = 0;
        agent.SetDestination(pathPoint[index]);
    }
}
