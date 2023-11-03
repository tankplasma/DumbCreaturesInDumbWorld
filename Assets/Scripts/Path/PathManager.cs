using System.Collections;
using UnityEngine;

public class PathManager : MonoBehaviour
{
    public GameObject agent;
    public int nmbOfAgent;
    public PathCheckpoint startPath;
    public GameObject spawnPoint;
    int agentsInstantiate = 0;
    public float timeBtwAgent = 1;

    public static PathManager instance;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        SpawnAgent();
    }

    void SpawnAgent()
    {
        agentsInstantiate++;
        GameObject obj = Instantiate(agent, spawnPoint.transform.position, Quaternion.identity);
        obj.GetComponent<AgentController>()?.ChangePath(startPath);
        StartCoroutine(WaitNextAgent());
    }

    IEnumerator WaitNextAgent()
    {
        yield return new WaitForSeconds(timeBtwAgent);
        if (agentsInstantiate < nmbOfAgent)
        {
            SpawnAgent();
        }
    }
}
