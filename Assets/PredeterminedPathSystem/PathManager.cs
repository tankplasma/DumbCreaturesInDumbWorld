using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.Splines;

public class PathManager : MonoBehaviour
{
    public static PathManager instance;

    public PathCheckpoint BegininPath;

    [SerializeField]
    GameObject DumbePrefab;

    [SerializeField]
    int NumberOfDumbies;

    [SerializeField]
    float SecondsBetweenSpawns;

    [SerializeField]
    Transform spawnPos;

    private void Awake()
    {
        if(instance)
            Destroy(instance);
        else
            instance = this;
    }

    private void Start()
    {
        StartCoroutine(SpawnDubies());
    }

    IEnumerator SpawnDubies()
    {
        int count = 0;

        while(count < NumberOfDumbies) 
        {
            SpawnDubie();
            count++;
            yield return new WaitForSeconds(SecondsBetweenSpawns);
        }
    }

    void SpawnDubie()
    {
        Instantiate(DumbePrefab, spawnPos.position , spawnPos.rotation);
    }

    /*[SerializeField]
    ScriptablePathCenter pathCenter;

    [SerializeField , HideInInspector]
    public List<NodeContainer> nodes = new List<NodeContainer>();

    public void AddNode(NodeContainer node)
    {
        nodes.Add(node);
        pathCenter.AddNode(node);
    }

    public void RemoveNode(NodeContainer node)
    {
        nodes.Remove(node);
        pathCenter.RemoveNode(node);
    }

    [SerializeField]
    ScriptableGameState gameState;

    public GameObject agent;
    public int nmbOfAgent;
    public PathCheckpoint startPath;
    public GameObject spawnPoint;
    int agentsInstantiate = 0;
    public float timeBtwAgent = 1;
    Spline spline;
    public static PathManager instance;

    void Awake()
    {
        
        instance = this;
    }

    void Start()
    {
        StartCoroutine(SpawnAgents());
    }

    void SpawnAgent()
    {
        agentsInstantiate++;
        AgentController obj = Instantiate(agent, spawnPoint.transform.position, Quaternion.identity).GetComponent<AgentController>();
        obj.ChangePath(startPath);
        if (gameState)
            gameState.AddPNJ(obj.main);
    }

    IEnumerator SpawnAgents()
    {
        for (int i = 0; i < nmbOfAgent; i++)
        {
            SpawnAgent();
            yield return new WaitForSeconds(timeBtwAgent);
        }
    }*/
}
