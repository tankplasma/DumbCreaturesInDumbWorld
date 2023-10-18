using PathCreation;
using UnityEngine;

public class PathManager : MonoBehaviour
{
    [SerializeField] PathCreator startPath;
    public AgentController agent;

    [ContextMenu("Function/Test")]
    void test()
    {
        agent.ChangePath(startPath);
    }
}
