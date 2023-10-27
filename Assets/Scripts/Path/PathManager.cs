using PathCreation;
using UnityEngine;

public class PathManager : MonoBehaviour
{
    public AgentController agent;
    public PathCheckpoint startPath;

    public static PathManager instance;

    void Awake()
    {
        instance = this;
    }

    [ContextMenu("Function/Test")]
    void test()
    {
        agent.ChangePath(startPath);
    }
}
